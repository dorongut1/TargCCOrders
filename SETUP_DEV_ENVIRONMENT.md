# הקמת סביבת פיתוח מקומית

**עודכן:** 18.8.2026
**מטרה:** להביא מפתח חדש למצב שבו הוא מריץ את המערכת מקומית מול DB מלא,
ורואה בדיוק את מה שהצוות רואה.

> קרא קודם את `HANDOVER_2026-08-18.md` — הוא מסביר **למה** הדברים בנויים כך.
> המסמך הזה הוא **איך**, צעד אחר צעד.

---

## 0. מה צריך להתקין

| רכיב | גרסה | הערה |
|---|---|---|
| **SQL Server** | 2019+ | Developer Edition מספיקה |
| **SQL Server Management Studio** | כל גרסה | לשחזור הגיבוי |
| **Visual Studio 2022/18** | Professional | **נדרש בגלל MSBuild** — ראה §5 |
| **.NET 8 SDK** | 8.0.x | |
| **Node.js** | 20+ | |
| **Git** | | |

⚠️ **אין `dotnet build` תקין בפרויקט.** `DBController.vbproj` בפורמט ישן
ורק MSBuild של Visual Studio יודע לבנות אותו. זו לא בחירה — זו מגבלה.

---

## 1. שכפול הקוד

```bash
git clone https://github.com/dorongut1/TargCCOrders.git C:\Dev\NonTFS\TargCCOrders
```

```bash
cd C:\Dev\NonTFS\TargCCOrders; git checkout develop
```

**`develop` הוא ענף העבודה.** `main` הוא מה שרץ בייצור — לא עובדים עליו ישירות.
ראה §8 לשיטת העבודה עם ענפים.

---

## 2. מסד הנתונים — כנראה כבר יש לך אחד

**אם קיבלת עותק מדורון בעבר — אינך צריך גיבוי חדש.** ה-DB לא נבנה מחדש מאז;
מה שהשתנה הוא סקריפטים בודדים, וכולם בריפו. הרץ קודם את הבדיקה ותדע בדיוק
מה חסר לך:

```bash
cd C:\Dev\NonTFS\TargCCOrders\Database; sqlcmd -S localhost -d TargCCOrdersNew -E -I -f 65001 -b -i "CHECK_DbState.sql"
```

הוא **קורא בלבד ואינו משנה דבר**, ומדפיס שורה לכל סקריפט:

```
[ok]   ADD_DeliveryMethods_2026-08-18.sql   already applied (18 rows)
[RUN]  CREATE_EnumMetadata_2026-08-18.sql   -- table missing
```

**כל מה שמסומן `[RUN]` — להריץ לפי §4, בסדר שהוא מציג.** מה שמסומן `[ok]` לדלג.

הוא גם מדפיס בדיקת תקינות:

| שורה | מה זה אומר |
|---|---|
| `procedures : 831` | תקין. **אם קרוב לאפס — יש לך עותק סכמה בלבד, והוא אינו שמיש** |
| `clr enabled : 1` | תקין. אם `0` — §3 |
| `customers : 1312` | הנתונים במקום |

### רק אם אין לך DB בכלל

⚠️ **`DB_SCHEMA.sql` שבריפו לא יביא אותך לשום מקום** — יש בו 41 טבלאות
ו**אפס פרוצדורות**, בעוד שה-DB החי מכיל **831**, ובנוסף נתוני מערכת
(`c_Enumeration`, `c_Role`, `c_Permission`, `c_User`) שבלעדיהם המערכת אינה
עולה כלל. נדרש גיבוי מלא.

דורון מפיק אותו כך:

```bash
sqlcmd -S localhost -E -I -b -Q "BACKUP DATABASE [TargCCOrdersNew] TO DISK='C:\Temp\TargCCOrdersNew.bak' WITH INIT, COMPRESSION, STATS=10;"
```

ואצלך:

```bash
sqlcmd -S localhost -E -I -b -Q "RESTORE DATABASE [TargCCOrdersNew] FROM DISK='C:\Temp\TargCCOrdersNew.bak' WITH REPLACE, RECOVERY, STATS=10;"
```

אם הנתיבים הפיזיים שונים אצלך השחזור ייכשל עם הודעה על `MOVE`. אז להריץ
`RESTORE FILELISTONLY FROM DISK='...'` ולחזור על ה-`RESTORE` עם
`MOVE 'שם_לוגי' TO 'C:\...\file.mdf'` לכל קובץ.

⚠️ **הגיבוי מכיל 1,312 לקוחות אמיתיים.** להעביר בשיתוף פנימי בלבד — לא במייל,
לא בענן ציבורי, ובשום מקרה לא לגיט.

---

## 3. ⚠️ SQLCLR — חובה, והכישלון שלו מבלבל

TargCC מסרב לבנות מחרוזת חיבור בלי SQLCLR מופעל.

**התסמין המבלבל:** לוג העלייה מדווח `Database connection OK - 41 tables visible`
וכל נקודת קצה נכשלת. אל תחפש את הבאג בקוד.

```bash
sqlcmd -S localhost -E -I -b -Q "EXEC sp_configure 'clr enabled', 1; RECONFIGURE;"
```

בדיקה:

```bash
sqlcmd -S localhost -E -I -b -h-1 -W -Q "SET NOCOUNT ON; SELECT 'clr enabled = ' + CAST(value_in_use AS VARCHAR) FROM sys.configurations WHERE name='clr enabled';"
```

חייב להחזיר `1`.

---

## 4. סקריפטי ה-DB של הענף

**להריץ בסדר הזה.** שניהם idempotent — הרצה חוזרת בטוחה ולא יוצרת כפילויות.

```bash
cd C:\Dev\NonTFS\TargCCOrders\Database; sqlcmd -S localhost -d TargCCOrdersNew -E -I -f 65001 -b -i "ADD_DeliveryMethods_2026-08-18.sql"
```

צפוי: `DeliveryMethod rows now: 18`

```bash
cd C:\Dev\NonTFS\TargCCOrders\Database; sqlcmd -S localhost -d TargCCOrdersNew -E -I -f 65001 -b -i "CREATE_EnumMetadata_2026-08-18.sql"
```

צפוי: `EnumMetadata rows: 156` · `Marked IsDelivery: 2`

> **על הדגלים:** `-I` נדרש כי `OrderHeader` דורש `QUOTED_IDENTIFIER ON`, ובלעדיו
> פרוצדורה שנוצרת **נכשלת בזמן ריצה בלי שום אזהרה ביצירה**.
> `-f 65001` נדרש לעברית. `-b` עוצר בשגיאה במקום להמשיך בשקט.

### אם ה-DB שקיבלת ישן יותר

יתכן שחסרים גם התיקונים הקודמים. הרץ אותם — גם הם בטוחים להרצה חוזרת:

```bash
cd C:\Dev\NonTFS\TargCCOrders\Database; sqlcmd -S localhost -d TargCCOrdersNew -E -I -f 65001 -b -i "FIX_VATRate_ServerSide_2026-08-16.sql"
```

```bash
cd C:\Dev\NonTFS\TargCCOrders\Database; sqlcmd -S localhost -d TargCCOrdersNew -E -I -f 65001 -b -i "FIX_ProcOrdinalDrift_2026-08-17.sql"
```

---

## 5. בנייה

### קונפיגורציה

`TargCCOrders.WebAPIHost\app.config` מצביע כברירת מחדל על:

```xml
<add key="TargCCOrders.Controller" value="Localhost~TargCCOrdersNew~500"/>
```

הפורמט הוא `שרת~מסד~timeout~משתמש~סיסמה`. עם Windows Authentication משאירים
את שני האחרונים ריקים. **אם ה-SQL שלך על instance בשם אחר** — לשנות רק את
החלק הראשון.

### .NET

```bash
& "C:\Program Files\Microsoft Visual Studio\18\Professional\MSBuild\Current\Bin\MSBuild.exe" "C:\Dev\NonTFS\TargCCOrders\TargCCOrders.WebAPIHost\TargCCOrders.WebAPIHost.csproj" -t:Build -v:m -p:Configuration=Debug
```

אם Visual Studio אצלך בגרסה אחרת — להתאים את `18\Professional` בנתיב.

### React

```bash
cd C:\Dev\NonTFS\TargCCOrders\TargCCOrders.ReactUI\ReactUI; npm install
```

```bash
cd C:\Dev\NonTFS\TargCCOrders\TargCCOrders.ReactUI\ReactUI; npx tsc --noEmit
```

חייב להיות **נקי לגמרי**.

```bash
cd C:\Dev\NonTFS\TargCCOrders\TargCCOrders.ReactUI\ReactUI; npx vitest run
```

צפוי: **20 בדיקות עוברות**.

```bash
cd C:\Dev\NonTFS\TargCCOrders\TargCCOrders.ReactUI\ReactUI; cmd /c "npm run build & echo EXIT=%ERRORLEVEL%"
```

⚠️ **`npm run build` מחזיר exit 1 ב-PowerShell גם כשהוא מצליח.** לכן דרך `cmd`
עם `ERRORLEVEL` — זו הדרך היחידה לדעת אם הוא באמת הצליח. חייב `EXIT=0`.

---

## 6. הרצה מקומית

⚠️ **השורה שהכי קל לשכוח.** ה-Publish וה-Build לא מעתיקים את ה-React:

```bash
cd C:\Dev\NonTFS\TargCCOrders; Remove-Item "TargCCOrders.WebAPIHost\bin\Debug\net8.0\wwwroot\*" -Recurse -Force -ErrorAction SilentlyContinue; New-Item -ItemType Directory -Force "TargCCOrders.WebAPIHost\bin\Debug\net8.0\wwwroot" | Out-Null; Copy-Item "TargCCOrders.ReactUI\ReactUI\dist\*" "TargCCOrders.WebAPIHost\bin\Debug\net8.0\wwwroot\" -Recurse -Force
```

**בלי זה תראה ממשק ישן ותרדוף אחרי באג שלא קיים.** מנקים את `wwwroot` לפני
ההעתקה כי נכסי build ישנים נשארים עם hash אחר ומצטברים.

הרצה:

```bash
cd C:\Dev\NonTFS\TargCCOrders\TargCCOrders.WebAPIHost\bin\Debug\net8.0; $env:ASPNETCORE_ENVIRONMENT='Development'; .\TargCCOrders.WebAPIHost.exe --urls http://localhost:5199
```

בדיקה בחלון אחר:

```bash
curl.exe -s http://localhost:5199/api/health
```

צפוי: `{"status":"ok","db":"ok",...}`

אם `db` אינו `ok` — חזור ל-§3 (SQLCLR) ול-§5 (מחרוזת החיבור).

---

## 7. אימות שהגעת למצב הנכון

פתח `http://localhost:5199` והתחבר.

### שלוש בדיקות הליבה — לא מדלגים

| בדיקה | צפוי | מאמת |
|---|---|---|
| מסך לקוחות | **1,312** שורות | סחיפת ordinal |
| הזמנה חדשה → שמירה | סטטוס **"חדש"**, לא "מבוטל" | ה-enum |
| פתיחה מחדש של אותה הזמנה | **מע"מ אינו אפס** | המע"מ |

### בדיקות הענף הזה

| בדיקה | צפוי |
|---|---|
| תפריט → `ניהול` → `ניהול פרמטרים` | המסך נטען, 5 טאבים |
| טאב `צורות משלוח` | **18 ערכים** |
| `אלקנה`, `YDM`, `באר טוביה`, `ב. גבריאל` | קיימים עם תוויות עבריות |
| אייקון משאית | על `אלקנה` ו`ליאור כרמיאל` **בלבד** |
| עריכת תווית → שמירה | מופיעה מיד ברשימה |

### רשת הביטחון של ה-enum

**הרץ אותה אחרי כל נגיעה ב-enums.** שלוש פקודות:

```bash
sqlcmd -S localhost -d TargCCOrdersNew -E -I -b -h-1 -W -Q "UPDATE OrderHeader SET enmDeliveryMethod='Elkana' WHERE ID=(SELECT MIN(ID) FROM OrderHeader);"
```

```bash
curl.exe -s -X POST http://localhost:5199/api/diagnostics/orderRoundTrip/<ID>
```

```bash
sqlcmd -S localhost -d TargCCOrdersNew -E -I -b -h-1 -W -Q "SELECT enmDeliveryMethod FROM OrderHeader WHERE ID=<ID>;"
```

**חייב לחזור `Elkana`.** אם חזר `UD` — חסרה אחת משלוש העריכות שנדרשות
להוספת ערך enum. ראה §3 ב-`HANDOVER_2026-08-18.md`.

הנקודה קיימת רק ב-Development ואינה דורשת התחברות בכוונה, כדי שתרוץ שוב
בלי להיות תלויה באדם.

---

## 8. שיטת העבודה עם ענפים

```
main       ← ייצור בלבד. מה שרץ על orders.target.co.il
  └ develop   ← אינטגרציה. כל מה שגמור ונבדק
      └ feature/<מזהה>-<תיאור>   ← משימה בודדת
```

**כללים:**

- **לא עובדים על `main` ישירות.** הוא משתנה רק כשמשהו נפרס לייצור.
- כל משימה מקבלת ענף משלה מ-`develop`, בשם לפי מזהה המשימה בתוכנית:
  `feature/1.4-import-customers`, `feature/2.1-order-intelligence`.
- כשמשימה גמורה **ואומתה חי** — ממזגים ל-`develop`.
- `develop` → `main` רק בפריסה לייצור.

```bash
git checkout develop; git pull; git checkout -b feature/1.4-import-customers
```

בסיום:

```bash
git checkout develop; git merge --no-ff feature/1.4-import-customers; git push
```

`--no-ff` שומר את המשימה כיחידה אחת בהיסטוריה — קל לראות מה נכנס ומתי,
וקל להחזיר אחורה.

⚠️ **לעולם לא `git add -A`.** בריפו יש קבצים עם 1,433 לקוחות אמיתיים
(`Database/import/`, `IMPORT_2_migrate_*.sql`) שמוחרגים ב-`.gitignore`.
להוסיף קבצים **בשמם המפורש**. לפני כל commit:

```bash
git status --porcelain | Select-String -Pattern "_token|Database/import/|IMPORT_2_migrate|\.csv$"
```

**חייב לחזור ריק.**

---

## 9. תקלות נפוצות

| תסמין | סיבה | פתרון |
|---|---|---|
| `db` אינו `ok` ב-health | SQLCLR כבוי | §3 |
| כל נקודת קצה נכשלת אך הלוג אומר "connection OK" | SQLCLR כבוי | §3 |
| `MSB3021: cannot access the file ... .exe` | השרת המקומי רץ ונועל את הקובץ | `Get-Process TargCCOrders.WebAPIHost \| Stop-Process -Force` |
| ממשק ישן אחרי build | לא הועתק `dist` ל-`wwwroot` | §6 |
| `?????` במקום עברית ב-sqlcmd | קידוד קונסולה | להוסיף `-f 65001`, או לכתוב לקובץ ולקרוא ב-Node |
| ערך enum הופך ל-`UD` | חסרה עריכה שלישית (`FastToString`) | §3 ב-HANDOVER |
| `PUT`/`DELETE` מחזירים 405 **בשרת** | WebDAV של IIS | `DEPLOY_TO_SERVER.md` §7 |
| הרצה של `npm run build` "נכשלת" ב-PowerShell | exit code שקרי | §5 — דרך `cmd` |

---

## 10. סדר הפעולות בקצרה

1. להתקין את §0
2. `git clone` + `git checkout develop`
3. להריץ את `CHECK_DbState.sql` ולהשלים מה שמסומן `[RUN]` (§2)
4. להפעיל SQLCLR (§3)
5. להריץ את שני סקריפטי הענף (§4)
6. `npm install` → `tsc` → `vitest` → `build` (§5)
7. להעתיק `dist` ל-`wwwroot` ולהריץ (§6)
8. לעבור על בדיקות §7

מגיע לשלב 8 ורואה 1,312 לקוחות ו-18 צורות משלוח — אתה במצב שלנו.
