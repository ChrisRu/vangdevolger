# Vang de Volger
Vang de Volger is een spelletje waarbij de speler achtervolgt wordt door een vijand. Dit gebeurt in een wereld die bestaat uit vakjes. In het basisspel doet de vijand elke seconde (of sneller!) een willekeurig stapje. De speler kan zo snel als hij of zij wil door het veld bewegen met behulp van de pijltjestoetsen.  De speler begint linksboven, de vijand rechtsonder.
Verder staan op het veld random blokken (ca. 5% of in te stellen) waar niet doorheen bewogen kan worden. Ook staan er dozen op het veld (ca 20% of in te stellen). Deze dozen kunnen door de speler geduwd worden, maar niet door de vijand. Ook kan een hele rij dozen in n keer geduwd worden.
Het spel is afgelopen als de vijand de speler raakt (verloren) of als de vijand niet meer kan bewegen (gewonnen).

## Opdracht:
Schrijf een grafische C# desktopapplicatie waarin Vang de Volger gespeeld kan worden. Denk hierbij aan het volgende:
* Naast het speelveld staan 2 knoppen: pauze/herstart en opnieuw (er wordt een nieuw veld gegenereerd). Eventueel is er een instellingen knop.
* Je levert zowel de applicatie op (met duidelijke becommentarieerde code) als een systeemontwerp (klassendiagram)
* Overerfde klassen uit libraries dienen als lege klasse te worden opgenomen in je ontwerp, zodat afhankelijkheden duidelijk worden
* Het systeemontwerp moet voldoen aan de kwaliteitseisen van ontwerp (voornamelijk lage koppeling). Denk aan responsibility driven design! Abstractielagen worden verwacht!
* Het veld is opgebouwd uit vakjes die elkaar vinden via een buren-attribuut. Cordinaten mogen enkel gebruikt worden bij het initieel opbouwen van het veld en bij het weergeven van het veld op het scherm
* Bedenk zelf hoe de verschillende game-events tussen de objecten worden uitgewisseld en waar de events initieel starten of binnenkomen.
* Maximaal cijfer is een 7 als bovenstaande in orde is. Voor een hoger cijfer:
* Een extra punt is te verdienen door het toevoegen van 2 of meer zelfbedachte interessante spelitems die het spel leuker maken. Overleg voordat je hiermee aan de slag gaat met de practicumleiding of de bedachte items inderdaad interessant genoeg zijn. (a priori: ze moeten de interactie met de andere spelitems benvloeden)
* Een extra punt is te verdienen door het gebruik van het pathfinding om de vijand ook daadwerkelijk in de richting van de speler te laten bewegen
* Het spel hoeft grafisch niet super aantrekkelijk te zijn. De spelelementen mogen bij wijze gekleurde vierkantjes zijn. Plaatjes zijn mooier. Natuurlijk geldt: hoe fraaier hoe beter, kortom; daag jezelf uit!

