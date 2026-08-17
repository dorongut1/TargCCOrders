# פריסה ראשונה לשרת — TargCCOrders

**נכתב:** 17.8.2026 · מבוסס על Publish שרץ בפועל (22.4MB, 133 קבצים) ועל בדיקות חיות מול `TargCCOrdersNew`.

> **ארכיטקטורה — חשוב להבין לפני שמתחילים:**
> זו **אפליקציה אחת**, לא שתיים. ה-WebAPIHost מגיש גם את ה-API וגם את ממשק ה-React
> מתוך `wwwroot` (אימתתי: `UseStaticFiles` + `MapFallbackToFile("index.html")`).
> לכן ב-IIS מגדירים **Site אחד** — לא אתר לפרונט ואתר ל-API.
>
> **המערכת רצה על Windows בלבד** — DBController משתמש ב-WMI, EventLog ו-Drawing.

---

## שלב 0 — מה צריך להיות מוכן

### 🔎 בדיקה אוטומטית — הרץ את זה קודם

במקום לעבור על הרשימה ידנית, העתק לשרת את `Deploy\Check-Prerequisites.ps1`
והרץ ב-PowerShell **כאדמין**:

```powershell
powershell -ExecutionPolicy Bypass -File .\Check-Prerequisites.ps1 `
    -PublishPath D:\Apps\TargCCOrders -SqlServer <SQLSERVER>
```

הסקריפט **רק בודק ומדווח** — הוא לא מתקין ולא משנה כלום. לכל כשל הוא מדפיס
את הפקודה המדויקת לתיקון. הוא מכסה:

| # | מה נבדק |
|---|---|
| 1 | גרסת Windows, והאם רצים עם הרשאות אדמין |
| 2 | .NET 8 Hosting Bundle **ורישום `aspnetcorev2.dll` ב-IIS** |
| 3 | IIS פעיל, ו-App Pool מוגדר `No Managed Code` |
| 4 | .NET Framework 4.8 (נדרש ל-DBController) |
| 5 | חיבור ל-SQL + **שהגיבוי אכן מכיל את התיקונים** (מע"מ, סדר עמודות, נתוני אב) |
| 6 | כל קובץ קריטי בתיקיית הפריסה, מחרוזת חיבור, והרשאת כתיבה ל-LogLocation |
| 7 | מפתח JWT ו-`ASPNETCORE_ENVIRONMENT` |

**סדר נכון:** הרץ פעם ראשונה לפני שמתחילים (יראה מה חסר להתקין), ופעם שנייה
אחרי ההעתקה והקונפיגורציה — עד שהוא מסיים ב-`0 blockers`.

> **שים לב לסעיף 2:** הסקריפט בודק לא רק ש-.NET 8 מותקן, אלא ש-`aspnetcorev2.dll`
> קיים. אפשר שה-Runtime מותקן וה-Module לא — וזה בדיוק המצב שמחזיר 500.19
> בלי הסבר. אם ה-Hosting Bundle הותקן **לפני** IIS, יש להתקין אותו מחדש.

### הרשימה עצמה

| | |
|---|---|
| שרת | Windows Server עם הרשאות אדמין |
| SQL Server | על השרת או נגיש ממנו |
| גישה | RDP לשרת |
| החלטה | האם יש תעודת SSL לדומיין? (אם לא — ראה שלב 8) |

---

## שלב 1 — התקנות בשרת (פעם אחת)

1. **.NET 8 Hosting Bundle** — לא ה-SDK ולא ה-Runtime הרגיל.
   רק ה-Hosting Bundle מתקין את `AspNetCoreModuleV2` שה-`web.config` שלנו דורש.
   חיפוש: "dotnet hosting bundle 8" → `dotnet-hosting-8.x.x-win.exe`
2. אחרי ההתקנה, ב-CMD כאדמין: `iisreset`
3. **IIS** — אם עדיין לא מותקן: Server Manager → Add Roles → Web Server (IIS)
4. **.NET Framework 4.8** — DBController בנוי עליו. מובנה ב-Windows Server 2019+, שווה לוודא.

**בדיקה שהשלב הצליח:** ב-IIS Manager → בחר את השרת → Modules → חפש `AspNetCoreModuleV2`.
אם הוא לא שם, ה-Hosting Bundle לא הותקן כמו שצריך ושום דבר לא יעבוד.

---

## שלב 2 — מסד הנתונים

> **קריטי:** חייבים **RESTORE מגיבוי**, לא בנייה מסקריפטים.
> תיקוני 9.8 (19 פרוצדורות) קיימים רק בתוך ה-DB ואין להם סקריפט בריפו.
> בנייה מסקריפטים תחזיר אותך חודש אחורה.

**במחשב שלך:**
```sql
BACKUP DATABASE TargCCOrdersNew
TO DISK = 'C:\Temp\TargCCOrdersNew.bak'
WITH INIT, COMPRESSION;
```

**בשרת** — העתק את ה-`.bak` והרץ RESTORE (ב-SSMS: Databases → Restore Database → Device).

אם קופצת שגיאת "principal dbo":
```sql
ALTER AUTHORIZATION ON DATABASE::TargCCOrdersNew TO sa;
```

**צור משתמש SQL ייעודי לאפליקציה** (אל תשתמש ב-sa):

```sql
CREATE LOGIN TargCCApp WITH PASSWORD = '<סיסמה חזקה>';
USE TargCCOrdersNew;
CREATE USER TargCCApp FOR LOGIN TargCCApp;
ALTER ROLE db_datareader ADD MEMBER TargCCApp;
GRANT EXECUTE TO TargCCApp;
```
TargCC עובד דרך פרוצדורות בלבד, ולכן `EXECUTE` מספיק — וזה גם מאובטח יותר.

**חלופה פשוטה יותר — Integrated Security.** במחרוזת החיבור משאירים שלושה חלקים
בלבד (`localhost~TargCCOrdersNew~500`), וה-App Pool ניגש בזהות שלו. אין סיסמה
בקובץ טקסט. במקרה כזה:
```sql
CREATE LOGIN [IIS APPPOOL\<שם-האתר>] FROM WINDOWS;
USE TargCCOrdersNew;
CREATE USER [IIS APPPOOL\<שם-האתר>] FOR LOGIN [IIS APPPOOL\<שם-האתר>];
ALTER ROLE db_datareader ADD MEMBER [IIS APPPOOL\<שם-האתר>];
GRANT EXECUTE TO [IIS APPPOOL\<שם-האתר>];
```

### ⚠️ SQLCLR — חובה, ולא ניתן לדלג

**TargCC מסרב לבנות מחרוזת חיבור כלל כל עוד SQLCLR מושבת.** השגיאה נזרקת
ב-`MyController.CreateDBConnString`, לפני כל שאילתה:

```
This application requires CLR to be enabled. Please contact your DBA.
```

**התסמין מבלבל במיוחד:** לוג העלייה מדווח `Database connection OK - 39 tables
visible`, ובכל זאת **כל** נקודת קצה מחזירה שגיאה. במופע SQL חדש CLR כבוי
כברירת מחדל.

```sql
EXEC sp_configure 'show advanced options', 1; RECONFIGURE;
EXEC sp_configure 'clr enabled', 1;           RECONFIGURE;
SELECT name, value_in_use FROM sys.configurations WHERE name LIKE 'clr%';
```
`clr enabled` חייב להיות `1`. אין צורך להפעיל מחדש את SQL Server.

אם `clr strict security = 1` (ברירת מחדל ב-SQL 2017+) ה-assembly של ה-Audit
של TargCC עלול להיחסם. אם ה-Audit לא מתפקד:
```sql
ALTER DATABASE TargCCOrdersNew SET TRUSTWORTHY ON;
EXEC sp_changedbowner 'sa';
```

**בדיקה שהשלב הצליח:**
```sql
USE TargCCOrdersNew;
SELECT COUNT(*) FROM sys.procedures;              -- צריך להיות מאות
SELECT TOP 3 ID, OrderNumber, VATRatePercent, clc_TotalWithVAT
FROM dbo.OrderHeader ORDER BY ID DESC;            -- VATRatePercent חייב להיות 18.00, לא 0
```
אם `VATRatePercent` הוא 0 — הגיבוי נלקח לפני תיקון המע"מ. חזור ל-BACKUP.

---

## שלב 3 — מפתח JWT (עשה את זה לפני ההעתקה!)

זו החוליה החלשה היחידה שנשארה. בלי זה — כל מי שראה את הריפו יכול לזייף טוקן אדמין,
**ולעקוף לחלוטין את כל 27 הקונטרולרים שנעלנו.**

צור מפתח:
```powershell
[Convert]::ToBase64String((1..48 | ForEach-Object { Get-Random -Maximum 256 }))
```

**אל תשים אותו ב-`appsettings.json`** — הקובץ בגיט, והמפתח ייכנס לריפו בקומיט הבא.
במקום זה, בשרת (PowerShell כאדמין):
```powershell
[Environment]::SetEnvironmentVariable('Jwt__AdminKey', '<המפתח שיצרת>', 'Machine')
```
שני קווים תחתונים — כך .NET ממפה למפתח המקונן `Jwt:AdminKey`.
אחרי זה: `iisreset`.

**החלף גם את סיסמת Admin** אם היא עדיין `password`.

---

## שלב 4 — בניית החבילה (כבר בוצע)

החבילה מוכנה ומאומתת ב-**`C:\Dev\Publish\TargCCOrders`** — 22.4MB, 133 קבצים,
כולל `wwwroot` עם ה-React, `web.config`, `System.Management.dll` ו-`TargCCOrders.DBController.dll`.

לבנייה מחדש בעתיד (אחרי שינויי קוד):
```powershell
cd C:\Dev\NonTFS\TargCCOrders\TargCCOrders.ReactUI\ReactUI
npm run build

cd C:\Dev\NonTFS\TargCCOrders
& "C:\Program Files\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin\MSBuild.exe" `
  "TargCCOrders.WebAPIHost\TargCCOrders.WebAPIHost.csproj" `
  -t:Publish -p:Configuration=Release -p:PublishDir=C:\Dev\Publish\TargCCOrders\

Copy-Item "TargCCOrders.ReactUI\ReactUI\dist\*" "C:\Dev\Publish\TargCCOrders\wwwroot\" -Recurse -Force
```
**שכחת את שורת ה-Copy = השרת יגיש גרסת React ישנה.** זו הטעות הכי קלה לעשות כאן.

---

## שלב 5 — העתקה לשרת

העתק את **כל** התיקייה `C:\Dev\Publish\TargCCOrders` לשרת, למשל ל-`D:\Apps\TargCCOrders`.

---

## שלב 6 — קונפיגורציה בשרת (שני קבצים)

### א. `TargCCOrders.WebAPIHost.dll.config`

```xml
<add key="TargCCOrders.Controller" value="<SQLSERVER>~TargCCOrdersNew~500~TargCCApp~<סיסמה>"/>
<add key="LogLocation" value="D:\Logs\TargCC"/>
```
הפורמט: `שרת~מסד~timeout~משתמש~סיסמה`. בלי שני האחרונים = Integrated Security.

**צור את תיקיית הלוגים ותן ל-App Pool הרשאת כתיבה** —
זו תקלה מס' 1 בפריסות כאלה, והיא מתבטאת ב-500.30 בלי הסבר:
```powershell
New-Item -ItemType Directory -Path D:\Logs\TargCC -Force
icacls "D:\Logs\TargCC" /grant "IIS AppPool\TargCCOrders:(OI)(CI)M"
```

### ב. `appsettings.json`

```json
"Cors": { "AllowedOrigins": [ "https://orders.yourcompany.com" ] }
```
הכתובת האמיתית של האתר. **את `Jwt:AdminKey` השאר כ-placeholder** — משתנה הסביבה משלב 3 גובר עליו.

---

## שלב 7 — IIS

1. **Application Pool** חדש בשם `TargCCOrders`:
   - .NET CLR Version = **No Managed Code**
     (נכון דווקא כי זו .NET 8 — ה-AspNetCoreModule מריץ אותה, לא ה-CLR של IIS)
   - Identity = ApplicationPoolIdentity
2. **Site** חדש:
   - Physical path = `D:\Apps\TargCCOrders`
   - Application Pool = `TargCCOrders`
   - Binding = ראה שלב 8
3. **משתנה סביבה** (אם לא הוגדר ב-Machine):
   ב-Configuration Editor של ה-Site → `system.webServer/aspNetCore` → environmentVariables →
   `ASPNETCORE_ENVIRONMENT` = `Production`
   (מכבה Swagger, מדליק HSTS ו-HTTPS redirect)

4. **לכבות את WebDAV — חובה.** בלי זה `PUT` ו-`DELETE` מחזירים **405** לכל
   נקודות הקצה, כולל עריכה ומחיקה של משתמשים. `WebDAVModule` רושם את עצמו
   כמטפל בשתי הפעולות האלה ועונה **לפני** ש-ASP.NET Core רואה את הבקשה.

   ב-`web.config` של האתר, בתוך `<system.webServer>`:

   ```xml
   <handlers>
     <remove name="WebDAV" />
     <!-- ה-add של aspNetCore נשאר אחרי זה -->
   </handlers>
   <modules>
     <remove name="WebDAVModule" />
   </modules>
   ```

   > **למה זה תופס דווקא בייצור:** מקומית האפליקציה רצה על Kestrel, שאין בו
   > WebDAV, ולכן הכל עובד. התסמין מופיע רק אחרי הפריסה ונראה כמו באג בקוד.
   > **הבדיקה המבחינה:** `PUT` שמחזיר **401** הגיע לאפליקציה ותקין;
   > **405** נחסם ב-IIS. אירע 17.8.2026.

   ⚠️ `web.config` אינו נכלל בחבילת הפריסה, ולכן התיקון **שורד פריסות**.
   אבל אתר שמוקם מאפס יקבל web.config חדש — והתקלה תחזור.

---

## שלב 8 — HTTPS

**יש תעודת דומיין?** ייבא ב-IIS → Server Certificates → Import, וקשר ל-Site ב-Binding על 443.

**אין?** שתי אפשרויות:
- **win-acme** (Let's Encrypt) — אם השרת נגיש מהאינטרנט
- **תעודה פנימית** — לדמו פנימי. הדפדפן יציג אזהרה בפעם הראשונה.

> **לדמו פנימי בלבד** אפשר גם בלי HTTPS, אבל אז אל תגדיר `ASPNETCORE_ENVIRONMENT=Production`
> (ה-HTTPS redirect יפיל את האתר). זו פשרה זמנית — לא לשימוש שוטף.

**Firewall:** פתח את הפורט הרלוונטי (443 או 80).

---

## שלב 9 — Job יומי (SQL Agent)

צור Job שרץ פעם ביום:
```sql
USE TargCCOrdersNew;
EXEC sp_UpdateDebtAttention;
EXEC sp_RefreshBeehiveTracking;
```
בלי זה: דוח החובות לא מתעדכן ומעקב קוני הכוורות קופא.

---

## שלב 10 — בדיקת עשן (אל תדלג!)

לפי הסדר. אם משהו נכשל — עצור שם, אל תמשיך.

| # | בדיקה | תוצאה נדרשת |
|---|---|---|
| 1 | `https://<כתובת>/api/health` | `{"status":"ok","db":"ok"}` |
| 2 | פתיחת האתר בדפדפן | מסך התחברות בעברית |
| 3 | התחברות | מגיע ללוח הבקרה |
| 4 | דף הזמנות נטען | רשימה, בלי 500 |
| 5 | **הזמנה חדשה עם 2 שורות** | סטטוס ברירת מחדל = **"חדש"**, לא "מבוטל" |
| 6 | סיכום בזמן הזנה | מע"מ 18% מוצג |
| 7 | שמירה, ואז פתיחה מחדש | **מע"מ וסה"כ אינם אפס** ← זה מאמת את תיקון המע"מ |
| 8 | מחיקת שורה + שמירה | הסכום מתעדכן ונשמר |
| 9 | תפריט → ניהול משתמשים | רשימה נטענת |
| 10 | יצירת משתמש + התנתקות + התחברות איתו (1234) | נכנס בהצלחה |
| 11 | אותו משתמש → ניהול משתמשים | **חסום** ← מאמת את הגידור |

**5, 7 ו-11 הן הקריטיות** — הן מאמתות את שלושת התיקונים הגדולים של הסבב הזה.

### אם משהו נשבר

| תסמין | סיבה כמעט תמיד |
|---|---|
| **500.30** בעליית האתר | connection string שגוי, או ל-App Pool אין כתיבה ל-LogLocation |
| **500.19** | ה-Hosting Bundle לא מותקן (`AspNetCoreModuleV2` חסר) |
| התחברות מחזירה 401 עם fault 60 | `System.Management.dll` לא הועתק — ודא שהוא ב-root של הפריסה |
| האתר עולה אבל ריק | `wwwroot` ריק — שכחת את שורת ה-Copy משלב 4 |
| כל בקשה 401 אחרי שהכל עבד | החלפת את מפתח ה-JWT — התחבר מחדש |

**איפה מסתכלים:** Event Viewer → Windows Logs → Application (שגיאות עליית האפליקציה),
ותיקיית `LogLocation` (שגיאות TargCC).

---

## שלב 11 — Rollback

נקודת השחזור המאומתת: **`cdba8cc`**.
```powershell
git checkout cdba8cc
```
בשרת: שמור עותק של תיקיית הפריסה הקודמת לפני כל עדכון. חזרה = החלפת תיקייה + `iisreset`.

---

## תזכורות קבועות

- **לא מריצים TargCC (CC)** על הפתרון. התיקונים למע"מ, ל-enums ולקונטרולרים
  יושבים בקבצים מיוצרים ו-CC ידרוס אותם. אם חייבים — עושים את זה יחד ובאופן מבוקר.
- כל שינוי: `commit` + `push` מיד.
- סקריפטי SQL על ה-DB הזה: תמיד `sqlcmd -I` או SSMS.
  `OrderHeader` דורש `QUOTED_IDENTIFIER ON`, ו-sqlcmd מכבה אותו כברירת מחדל —
  פרוצדורה שנוצרת ככה נכשלת בזמן ריצה בלי אזהרה בזמן היצירה.
