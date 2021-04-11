Fredrik Parkell - SUT20 - 2021-04-10

I arbetet med min tenta för kursen Avancerad .NET har jag arbetat för att uppnå en VG-nivå. Detta har jag gjort genom att implementera och jobba utifrån VG-exempel 2 och 3 som finns med i uppgiftsbeskrivningen. Dessa ligger beskrivna nedanför:

● Exempel 2 - Du visar och motiverar hur logik kan förläggas till databasen med hjälp av tex stored procedures samt hur du får dessa att samverka med EF genom att lägga till dessa i SQL-databasen och anropa dem för att göra vissa tillståndsförändringar. Du skriver även ihop en kortfattad readme-fil i vilken du motiverar och argumenterar för varför du valt att göra
som du gjort.
● Exempel 3 - Du planerar för, motiverar och genomför uthämtning/generering av statistik över hamstrarnas tid på dagis med hjälp av LINQ. Rapporterna kan vara enkla textrapporter till skärm eller fil.Du skriver även ihop en kortfattad readme-fil i vilken du motiverar och argumenterar för varför du valt att göra som du gjort.

------------------------------------

Enligt exempel 2 (Stored Procedures) har jag valt att göra 3 stored procedures. Den stora anledningen till att jag har valt att lägga logiken för just de operationerna är för att jag har tolkat det som en performance fråga när det kommer till att utföra "bulk-updates/operations". Det vill säga, att sätta 30 hamstrars CageId till null kan göras genom en for/foreach-loop som kör igenom och sätter varje hamsters CageId till null var för sig, medans jag istället för detta gör samma sak med en UPDATE-SQL-query i min stored procedure.

Dessa stored procedures har jag gjort genom att skapa en migration och bygga mina stored procedures i dom. Dessa hänger sedan med när man dubbelkollar om databasen finns (och skapar den ifall den inte gör det, inkluderat mina stored procedures) i samband med att man drar igång en simulation.

Funderade även på att lägga till mer logik, bland annat skapandet av ActivityLogs, som stored procedures men valde att nöja mig med 3 stycken för att visa att jag kan göra både och. Tycker även att det är väldigt smidigt och bra att det finns mer än ett sätt att göra det på. Vilken form av logik som är mest lämplig eller som man själv väljer att lägga direkt i databasen går det att skriva långa reflektioner kring, men just på grund av vad bland annat länken nedan nämner så känner jag att jag åtminstone gjort rätt i att göra de stored procedures som jag har gjort.

https://docs.microsoft.com/en-us/ef/core/performance/efficient-updating

------------------------------------

Enligt exempel 3 (LINQ) har jag använt mig av LINQ genomgående i mitt program. Främst i backend-delen, men även lite i frontend. Jag har bland annat en SimulationInfo-klass i backend som med hjälp av min UserPrint-klass i frontend skriver ut tick-rapporter, dag-rapporter och total-rapporter via "request" på en specifik simulation (har ett menyval "Look at specific simulation" som ger användaren möjlighet att kolla på gamla simulationers dag- och total-rapporter).

Min UserPrint-klass används också under simulationens gång i form av att LINQ-querys plockar ut information i CareHouseSimulation-klassen och via event skickar ut den för utskrift (tick-rapporter, dag-rapporter och total-rapporter).

Utöver det så har jag även byggt en egen generic LINQ-extension-metod (Shuffle) som ligger i LinqExtensions.cs-filen. Metoden returnerar en "shufflad" collection som jag använder i samband med att hamstrarna ska in i motionsytan för första gången på morgonen och alla hamstrar då har null-värden på sin LastExerciseTime. Detta gör jag för att det ska bli mer randomiserat och inte alltid bli samma hamstrar som varje morgon får träna först.

------------------------------------

Saknar jag något i min argumentation/reflektion kring VG-nivån jag har jobbat mot tar jag gärna och förklarar vidare vid följdfrågor.