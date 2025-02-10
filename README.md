SYSMC LTD - מערכת לניהול לקוחות
📌 הקדמה
פרויקט זה פותח עבור SYSMC LTD כמערכת לניהול לקוחות, בהתאם לדרישות שניתנו.
המערכת מאפשרת ניהול לקוחות, כתובות ואנשי קשר תוך שימוש בטכנולוגיות מודרניות.

🛠️ טכנולוגיות וארכיטקטורה
Frontend: Angular 18 (TypeScript, Angular Material)
Backend: ASP.NET Core 8 (Web API, Entity Framework Core)
Database: Microsoft SQL Server
📌 מבנה המערכת
1️⃣ תפריט ניווט ראשי

מעבר לרשימת הלקוחות
לחיצה על לקוח מציגה את פרטיו, הכתובות ואנשי הקשר
2️⃣ ניהול נתונים (CRUD)

יצירה, עדכון ומחיקה לוגית של לקוחות
הוספת כתובות ואנשי קשר ללקוחות קיימים
ולידציה לשמירה על תקינות הנתונים
3️⃣ רכיבים נוספים

Footer: מציג מידע ארגוני רלוונטי
התממשקות ל-API: כל הנתונים מנוהלים דרך Web API
🚀 התקנה והרצה
1️⃣ שכפול הריפו:

git clone https://github.com/DotanMezushan/SYSMC-LTD.git
2️⃣ הרצת צד לקוח (Angular):

cd SYSMC-LTD
cd sysmc-ltd
npm install
ng s -o
3️⃣ הרצת צד שרת (API):

לפתוח את הפרויקט SYSMCLTD_API ב- Visual Studio
להריץ את המערכת מול מסד הנתונים MS SQL - localhost
💡 אפשרות להגדרת בסיס נתונים:
ניתן להשתמש ב- EF Migrations

Add-Migration InitialCreate
Update-Database
או להפעיל seeding דרך קובץ program.cs ליצירת נתונים התחלתיים.

⚡ המערכת פותחה בהתאמה לדרישות הפרויקט, תוך שמירה על סטנדרטים גבוהים של קוד וניהול נתונים.
