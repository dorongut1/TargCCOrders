# כלי ייבוא נתונים מהאקסל הישן

סקריפטי Node חד-פעמיים ששימשו לייבוא נתוני האב מ-
`גיבוי של מערכת תיפעולית -הזמנות 2022 (29_5_2023).xlsx`.

נכתבו ב-Node ולא ב-SQL טהור כי `sqlcmd` הורס עברית בפלט הקונסולה
(`?????`), בעוד ש-Node קורא ומייצר UTF-8 כמו שצריך.

**הרצה:** מתוך `TargCCOrders.ReactUI/ReactUI` (שם מותקן `exceljs`):
```powershell
node ..\..\Database\tools\survey.cjs
```

| קובץ | תפקיד |
|---|---|
| `xlhelp.cjs` / `csv.cjs` / `genhelp.cjs` / `fixhelp.cjs` | עזר משותף |
| `survey.cjs` / `survey2.cjs` | סקירת מבנה הגיליונות ואיתור שורת הכותרת האמיתית |
| `qa.cjs` | דוח איכות נתונים לפני ייבוא |
| `export.cjs` | ייצוא 3 הגיליונות ל-CSV (**פלט מוחרג מגיט — מידע אישי**) |
| `gen.cjs` | ייצור סקריפט ההעברה (**פלט מוחרג מגיט — מידע אישי**) |
| `verify.cjs` / `verify2.cjs` / `e2e.cjs` | אימות אחרי ייבוא |
| `fixcust.cjs` / `fixdebt.cjs` | תיקון סחיפת ordinal בפרוצדורות |
| `dumpprocs.cjs` | ייצוא הפרוצדורות המתוקנות ל-`FIX_ProcOrdinalDrift` |

## סדר ההרצה

1. `survey.cjs` → `survey2.cjs` → `qa.cjs` — הבנה ובדיקת איכות
2. `export.cjs` — ייצור CSV ל-`Database/import/`
3. `IMPORT_1_staging_*.sql` — טעינה ל-`stg_` (sqlcmd -I)
4. `gen.cjs` → `IMPORT_2_migrate_*.sql` — העברה בטרנזקציה אחת (sqlcmd -I -f 65001)
5. `IMPORT_3_cleanup_*.sql` — ניקוי ומחיקת ה-staging
6. `verify.cjs` / `e2e.cjs` — אימות

## למה `IMPORT_2` לא בגיט

הוא מכיל 1,433 לקוחות אמיתיים כערכים מוטמעים — שמות, טלפונים, אימיילים,
כתובות וח.פ. אפשר לייצר אותו מחדש מהאקסל בכל רגע.

## אזהרה

הרצת TargCC (CC) תייצר מחדש את הפרוצדורות ותחזיר את סחיפת ה-ordinal.
לאחר כל הרצה כזו יש להריץ שוב את `FIX_ProcOrdinalDrift_2026-08-17.sql`
ואת `FIX_VATRate_ServerSide_2026-08-16.sql`.
