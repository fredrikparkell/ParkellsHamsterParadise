Fredrik Parkell - SUT20 - 2021-04-10

Under projektets gång har jag arbetat med, använt mig av och inkluderat bland annat:

-Visual Studio
-Git, GitHub & Git Fork (GitBash-interface, med 50+ commits)
-MSSMS (SQL Server, främst för att titta på data)
-LINQPad (för lite test)
-LucidChart (för att utforma ett UML)

-Delat upp mitt program i flera projekt/assemblies (tre stycken)
-Entity Framework Core
	-Egen DbContext
	-Egna Entities med relationer
	-Migrations
	-OnModelCreating/modelbuilder för att bland annat hantera vissa 		constraints (har bland annat check constraints på CageSize i 				både Cage- och ExerciseArea)) men också relationer
	-Database Seeding
-Använt mig av NugetPackages som behövs för Entity Framework Core

-Egna/Custom Extension-methods (LinqExtensions.cs & ModelBuilderExtensions.cs)
-Generics
-Events (fyra stycken)
-Egna/Custom EventArgs (fyra stycken)
-Timer (en sådan, såg inte behovet av fler. Har även ett sätt att pausa/återstarta timern)
-Trådhantering och async-metoder med Tasks och await-calls
-LINQ (endast i form av metoder, har inte gjort några "raw linq queries")

-Möjligt att jag missat nått mer.

-------------------------------------

Positiva reflektioner/extra nöjd med:

Något som jag är väldigt nöjd med att jag var snabb med att tänka på i början av planeringen var min inkludering av tabellen/entityn Simulation. Genom att ge varje ActivityLog ett simulations-id kan man undvika att blanda gamla ActivityLogs från äldre simulationer med de som är aktuella för den simulation som du kör, något som kan påverka att man får felaktig information om man inte rensar sina ActivityLogs genom typ en truncate inför varje ny simulation. Med simulations-id:t på ActivityLog delar jag istället upp en simulations loggar på en simulation och kan (vilket jag gör genom min SimulationInfo-klass) ta ut information om specifika gamla simulationer.

Frontend-bit men min dynamiska UserMenu-klass. Mycket smidig och jag gillar hur jag fick in ett alternativ istället för att användaren själv måste skriva in värden. Här räcker det med att navigera med pilarna och enter på tangentbordet.

Databasen och dess struktur. Mycket nöjd med den med tanke på att det är första gången man har använt ett ORM-ramverk alls och i synnerhet då Entity Framework. Intressant hur man kan bygga constraints genom modelbuilder men också hur man kan skapa stored procedures direkt i C#-kod.

-------------------------------------

Mindre nöjd med:

Tänkte först implementera en funktion som var kopplad till min Owner-Entitys Email-property/column, men det blev inte av. Därav är den tabellen lite "naken" förutom just Email och Name.

Har varit lite halvdålig på att kommentera min kod, framförallt på detaljnivå, men jag tycker att jag på nästan alla ställen använder bra metod- och klass-namn vilket "makes up for that".

Simulationer körs bra och snabbt upp till 7 ticks per sekund (142 ms/tick, smällde 4 gånger på 40+ testsimulationer med 7 ticks per sekund på både min laptop och desktop och en range på 2-8 dagar per simulation), men hinner inte alls med vid 8. Inga krascher på 5 ticks per sekund (20+ tester) eller 6 ticks per sekund (30+ tester) och inte heller något på 1-4 ticks per sekund. 

Möjligtvis att man hade kunnat optimera nån bit av koden för att få det att bli möjligt att köra några millisekunder snabbare. Samtidigt är det ganska snabbt redan vid 4-5, vilket nog är vad jag hade rekommenderat att köra på som mest för att hinna se utskriften (även om du kan pausa/återstarta under simulationens gång eller se rapporter i efterhand via mina "Look at specific simulation"-funktioner).