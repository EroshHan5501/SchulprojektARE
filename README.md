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
SchulprojektARE\Binaries
### ApiCaller
SchulprojektARE\Binaries\ApiCaller
Zieht die JSON aus der PokeAPi und deserialisiert diese in C#-Klassen. 
Diese werden dann mittels DBTransition(In der Common.dll) in die Datenbank geschrieben.
Hierfür muss man lediglich die apicaller.exe ausführen. Das Programm sollte eigenständig einen neuen Nutzer für MariaDB erstellen.
Aufgrund des Umfangs der Datensätze, wird dieser Prozess eine Weile dauern(Ca. 10 min auf meinem Gerät).
### WebApi
SchulprojektARE\Binaries\publish
Um die Daten mittels Zurgiff auf die API, als JSON, wiederzugeben, musst man die webapi.exe ausführen. Hierbei wird ein lokaler server gestartet. 
Hierfür muss die Datenbank bereits mittels ApiCaller befüllt worden sein.
Die Konsole wird die Addresse mit der entsprechenden URL anzeigen.(E.g. http://localhost:5000).
Um jetzt zugriff auf die JSON zu bekommen, muss man die gegebene URL um folgende Parameter ergänzen:
/api/Pokemon/?pageNumber=1&pageSize=20

Letztenendes sollte die Addresse die man über Browser oder Postman erreichen mchte, in etwa so aussehen:
http://localhost:5000/api/Pokemon/?pageNumber=1&pageSize=20
### DBTesting
SchulprojektARE\Binaries\dbtesting
