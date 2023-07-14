# SchulprojektARE

git config --global http.proxy 10.23.2.73:@proxy.its-stuttgart.de:3128

## We are crazy

API:
https://pokeapi.co/

Database Hoster: https://remotemysql.com/

## User
EroshHan5501/erenuezer = Eren Üzer /
AnnikaGommel = Annika Gommel /
Valacor = Ron Brütsch 

## Run Projects:
Die Projekte sind als Executables im Binaries Folder zu finden
Bevor man die Programme ausführt, muss man mysql in der Kommandozeile ausführen und mit dem source-Command das SQL-Skript ausführen.
SchulprojektARE\Binaries
### ApiCaller
SchulprojektARE\Binaries\ApiCaller
Zieht die JSON aus der PokeAPi und deserialisiert diese in C#-Klassen. 
Diese werden dann mittels DBTransition(In der Common.dll) in die Datenbank geschrieben.
Hierfür muss man lediglich die apicaller.exe ausführen.
Aufgrund des Umfangs der Datensätze, wird dieser Prozess eine Weile dauern(Ca. 10 min auf meinem Gerät).
### WebApi
SchulprojektARE\Binaries\publish
Um die Daten mittels Zurgiff auf die API, als JSON, wiederzugeben, musst man die webapi.exe ausführen. Hierbei wird ein lokaler server gestartet. 
Hierfür muss die Datenbank bereits mittels ApiCaller befüllt worden sein.
Die Konsole wird die Addresse mit der entsprechenden URL anzeigen.(E.g. http://localhost:5000).
Um jetzt zugriff auf die JSON zu bekommen, muss man die gegebene URL um folgende Parameter ergänzen:
/api/Pokemon/?pageNumber=1&pageSize=20. Leider gibt es noch einen Bug in dem Endpoint wodurch es nur möglich ist, einen Request an die API zu senden.
Über /api/Pokemon/detail/?pokemonId=<id> können beliebige Pokemon abgerufen werden.
Mit http://localhost:5000/index.html kann man die Website aufrufen

Letztenendes sollte die Addresse die man über Browser oder Postman erreichen möchte, in etwa so aussehen:
http://localhost:5000/api/Pokemon/?pageNumber=1&pageSize=20
### DBTesting
SchulprojektARE\Binaries\dbtesting
Einfach die Executable ausführen
