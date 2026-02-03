# API Introductie Opdracht — Console Weather App (OpenWeatherMap)

## Doel
Je bouwt een C# Console App die via de OpenWeatherMap API weergegevens ophaalt en netjes toont aan de gebruiker.
Je oefent hiermee: GET-requests, query parameters, JSON models, foutafhandeling, config/secrets en eenvoudige business logic.

## Voorkennis
- Je hebt basiskennis van HTTP GET en async/await.
- Je kunt een C# console project aanmaken en runnen.

Handige links mocht je de voorkennis missen:
- `https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient`.

## Setup

Maak een OpenWeatherMap account aan: `https://home.openweathermap.org/users/sign_up`. Vergeet je e-mailadres niet te valideren. Er word automatisch
een API sleutel aan je account toegewezen. Het kan even duren voordat deze ook echt actief is na uitgave.

Het is aan te raden om Postman te installeren (`https://www.postman.com/`). Postman is een tool waarmee je makkelijk en snel kan testen, ontwerpen, documenteren en
distribueren van APIs.

### Clone de repository
Haal het template om mee te werken uit deze repository op: `https://github.com/Ascrofft/OpenWeatherApiTemplate`.

### Restore project instellingen
Voer je dit uit in de project root:
```bash
cd OpenWeatherApiTemplate
dotnet restore
```

### appsettings.json toevoegen
Maak in de root van je project een bestand `appsettings.json`:
```json
{
	"OpenWeather": {
		"BaseUrl": "https://api.openweathermap.org/data/2.5",
		"TimeoutSeconds": 10
	}
}
```

### Toevoegen aan .csproj
Voeg dit toe aan je `.csproj`
```xml
<ItemGroup>
	<None Update="appsettings.json">
		<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
	</None>
</ItemGroup>
```

### User Secrets instellen (API key veilig opslaan)
Voer in de projectmap uit:
```bash
dotnet user-secrets init
dotnet user-secrets set "OpenWeather:ApiKey" "VERVANG_MET_JE_API_KEY"
```

Controleer of het is opgeslagen
```bash
dotnet user-secrets list
```

### Belangerijk
- API key nooit hardcoden
- Gebruik `dotnet user-secrets` voor `OpenWeather:ApiKey`
- Gebruik `appsettings.json` voor `OpenWeather:BaseUrl`

###
Start de applicatie met:
```bash
dotnet run
```

De applicatie zou nu zonder foutmeldingen moeten runnen.
Je zou "Hello World!" in de console moeten zien verschijnen.

---

## Opdracht 1 - Basis console app + menu
Bouw de basis van de applicatie: een console menu dat blijft herhalen tot de gebruiker afsluit. De gebruiker kan een keuze maken (bijv. 1/2/3/0),
vervolgens vraagt de app input (zoals een stad). Je hoeft nog geen echte API-calls te doen; focus op de flow, input lezen, simpele validatie
(lege input opnieuw vragen) en een nette “exit” optie.

- [ ] Opdracht 1 afgerond

#### Voorbeeld
```bash
=== Weather Console ===
1. Bekijk weer voor vandaag
2. Bekijk verwachting komende 24 uur
3. Weeradvies (Wat trek je aan?)
0. Afsluiten

Kies een optie: 
```

#### Definitie van afgerond
Je kunt de app starten, het menu verschijnt, ke kunt keuzes invullen en je kunt veilig afsluiten met optie `0` zonder crash.

---

## Opdracht 2 - Bekijk het weer van vandaag
Implementeer de eerste menu-optie: de gebruiker voert een stad in en krijgt het huidige weer terug in de console. Maak hiervoor een nette structuur
met minimaal:
- een Client (bijv. `OpenWeatherClient`) die de HTTP GET doet.
- een Service (bijv. `WeatherService`) die de client aanroept en het resultaat "vertaalt" naar iets dat je kunt printen.
- `Program.cs` blijft vooral flow + UI (menu, input, output)

Je output hoeft niet fancy te zijn, maar moet minimaal iets tonen zoals temperatuur + beschrijving.

- [ ] Opdracht 2 afgerond

#### Voorbeeld
```bash
=== Weather Console ===
1. Bekijk weer voor vandaag
2. Bekijk verwachting komende 24 uur
3. Weeradvies (wat trek je aan?)
0. Afsluiten

Kies een optie: 1
Geef locatie op (stad): Zwolle

====================
  Weer in Zwolle
====================
Temperatuur  : 3,9 °C
Voelt als    : -0,6 °C
Beschrijving : broken clouds
Wind         : 6,3 m/s
Luchtvocht.  : 77 %
Druk         : 1004 hPa

Druk op ENTER om door te gaan...
```

#### Definitie van afgerond
De gekozen menu-optie werkt end-to-end (input -> API -> output), de juiste OpenWeatherMap endpoint wordt gebruikt en de gebruiker krijgt een duidelijk
leesbaar resultaat of foutmelding in de console te zien.

---

## Opdracht 3 - Bekijk verwachting komende 24 uur
Implementeer de tweede menu-optie: de gebruiker voert een stad in en ziet een verwachting voor de komende 24 uur. OpenWeatherMap forecast komt meestal
in blokken van 3 uur. Toon daarom bijvoorbeeld de eerste 8 blokken (8 * 3 = 24 uur).

Werk opnieuw via de lagen:
Client -> Service -> Program.cs

- [ ] Opdracht 3 afgerond

#### Voorbeeld
```bash
=== Weather Console ===
1. Bekijk weer voor vandaag
2. Bekijk verwachting komende 24 uur
3. Weeradvies (wat trek je aan?)
0. Afsluiten

Kies een optie: 2
Geef locatie op (stad): Zwolle

=====================================
  Forecast komende 24 uur: Zwolle
=====================================
Tijd               Temp    Weer
-------------------------------------------------------
02-02 16:00         2,7°C  broken clouds
02-02 19:00        -0,2°C  scattered clouds
02-02 22:00        -3,2°C  few clouds
03-02 01:00        -4,2°C  few clouds
03-02 04:00        -5,1°C  few clouds
03-02 07:00        -4,5°C  few clouds
03-02 10:00        -2,3°C  scattered clouds
03-02 13:00         1,1°C  broken clouds

Samenvatting:
- Min temp: -5,1 °C
- Max temp: 2,7 °C
- Meest voorkomend: Clouds

Druk op ENTER om door te gaan...
```

#### Definitie van afgerond
Optie 2 werkt end-to-end en een duidelijke 24-uurs weersverwachting toont op basis van de OpenWeatherMap forecast data.

---

## Opdracht 4 - Celsius & Fahrenheit
Breid optie 1 (en bij voorkeur ook optie 2) uit zodat de gebruiker kan kiezen of hij Celsius of Fahrenheit wil zien. Laat de gebruiker kiezen via een
simpele input (bijv. `1 = Celsius`, `2 = Fahrenheit`). Deze keuze moet je vertalen naar de juiste query parameter.

- [ ] Opdracht 4 afgerond

#### Voorbeeld
```bash
=== Weather Console ===
1. Bekijk weer voor vandaag
2. Bekijk verwachting komende 24 uur
3. Weeradvies (wat trek je aan?)
0. Afsluiten

Kies een optie: 1
Geef locatie op (stad): Zwolle
Kies eenheid:
1. Celsius (°C)
2. Fahrenheit (°F)

Keuze: 1

====================
  Weer in Zwolle
====================
Temperatuur  : 3,9 °C
Voelt als    : -0,6 °C
Beschrijving : broken clouds
Wind         : 6,3 m/s
Luchtvocht.  : 77 %
Druk         : 1004 hPa

Druk op ENTER om door te gaan...
```

#### Definitie van afgerond
De gebruiker kan selecteren uit Celsius en Fahrenheit. Het resultaat wordt in de juiste eenheid getoond.

---

## Opdracht 5 - Weeradvies (Wat trek je aan?)
Implementeer de derde menu-optie: de gebruiker voert een stad in en krijgt een adviest bericht terug op basis van de API response. Denk aan eenvoudige
regels zoals:
- Koud -> "Brrr, trek een dikke jas aan"
- Regen -> "Neem een paraplu mee"
- Wind -> "Veel wind: houd rekening met stevige windstoten"

Implementeer minstens de 3 hierboven genoemde regels. Voor extra punten mag je er altijd meer implementeren.
Maak dit vooral leuk, maar wel gebaseerd op echte data (temperatuur, `weather.main`, description, wind)

- [ ] Opdracht 5 afgerond

#### Voorbeeld
```bash
=== Weather Console ===
1. Bekijk weer voor vandaag
2. Bekijk verwachting komende 24 uur
3. Weeradvies (wat trek je aan?)
0. Afsluiten

Kies een optie: 3
Geef locatie op (stad): Zwolle

========================
  Weeradvies: Zwolle
========================
Nu: 3,9 °C, scattered clouds, wind 6,5 m/s

- Het is koud: draag een warme jas.

Druk op ENTER om door te gaan...
```

#### Definitie van afgerond
De gebruiker kan ten minste zien of het hard/zacht waait, warm/koud is en wel of niet regent.

---

## Opdracht 6 - Nettere foutmeldingen (API errors)
Zorg dat je app duidelijke foutmeldingen geeft als de API een error terugstuurt. De app mag niet crashen met een stacktrace, maar moet iets tonen dat
je als developer ook kunt debuggen.

- [ ] Opdracht 6 afgerond

#### Voorbeeld
```bash
=== Weather Console ===
1. Bekijk weer voor vandaag
2. Bekijk verwachting komende 24 uur
3. Weeradvies (wat trek je aan?)
0. Afsluiten

Kies een optie: test
Ongeldige keuze. Kies 0-3.

Druk op ENTER om door te gaan...
```

```bash
=== Weather Console ===
1. Bekijk weer voor vandaag
2. Bekijk verwachting komende 24 uur
3. Weeradvies (wat trek je aan?)
0. Afsluiten

Kies een optie: 1
Geef locatie op (stad): 5

? Er ging iets mis:
404 Not Found: Stad niet gevonden.
{"cod":"404","message":"city not found"}

Druk op ENTER om door te gaan...
```

#### Hints
- Toon de statuscode en de response body van de API.
- Specifieke gevallen:
	- 401 -> API key ongeldig/niet geactiveerd
	- 404 -> Stad niet gevonden
	- 429 -> Te veel requests (rate limit)

#### Definitie van afgerond
De applicatie mag niet crashen. Duidelijke leesbare foutmeldingen worden teruggegeven aan de gebruiker.

---

## Opdracht 7 - Minimale in-memory cache
Voeg een simpele in-memory cache toe, zodat herhaalde requests binnen bijvoorbeeld 5 minuten niet telkens opnieuw de API aanroepen.

Waarom doen we dit?
- Minder API calls (rate limit / quota)
- Snellere response voor dezelfde vraag
- Nettere UX (vooral tijdens testen)

- [ ] Opdracht 7 afgerond

#### Hints
- Cache key kan zijn: `current:{city}:{unit}` of `forecast:{city}:{unit}`
- Cache value = jouw resultaat object
- Cache expiry = `DateTimeOffset.UtcNow` + TTL

#### Definitie van afgerond
Herhaalde identieke requests binnen de ingestelde TTL doen geen nieuwe API-call, maar gebruikt data uit een in-memory cache. Hetzelfde resultaat
moet verschijnen in de console.
