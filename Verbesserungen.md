# Wie geht es weiter
* In Verbindung mit einem Dienst wie Google Maps wollten wir einen dynamischen Pokedex entwickeln. Das bedeutet, dass am Anfang jeder Benutzer nur seine Starterpokemon in seinem Pokedex vorfindet. In der realen Welt werden dann
zufällig an Koordinaten Pokemon hinterlegt. Bewegt sich ein Spieler über diese Koordinate und hat die Website geöffnet, so wir das Pokemon in seinem Pokedex freigeschaltet. Schwache und normale Pokemon sollen dabei häufiger gefunden
werden als seltene und legendäre Pokemon. 

* Es soll auf der Website ermöglicht werden, dass Benutzer ihre eigenen Pokemon erstellen und anschließend auf unserer Web-API hochladen können. Diese werden dann ebenfalls aufgenommen und können von anderen Spielern entdeckt werden.

* Innerhalb des ApiCallers wäre es möglich, den Objektgenerierungs- und Datenbankbefüllungsprozess zu beschleunigen. Derzeit sind diese Prozesse im ApiCaller sehr Zeitaufwändig, da sequentiell multible Schleifen ausgeführt werden und große Mengen an Datensätzen und Prozesse behandeln müssen. Hier könnte eine Verkürzung erfolgen, indem man die Schleifen im ApiCaller in einer For-Schleife mit modifizierten Abbruchbedingungen zusammenfasst.