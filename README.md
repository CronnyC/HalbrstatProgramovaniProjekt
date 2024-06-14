# Dokumentace hry 2048

## Přehled

Tento dokument poskytuje přehled a dokumentaci pro hru 2048 implementovanou v jazyce C# s využitím WPF (Windows Presentation Foundation).

## Funkce

- **Nastavení mřížky**: Inicializuje mřížku tlačítek reprezentující herní dlaždice.
- **Herní logika**: Zpracovává vstupy uživatele (šipky) pro pohyb a slučování dlaždic.
- **Udržování skóre**: Sleduje a zobrazuje aktuální skóre na základě hodnot dlaždic.
- **Zpracování konce hry**: Detekuje a zpracovává podmínky konce hry.
- **Barevné kódování**: Barevně zobrazuje dlaždice na základě jejich numerických hodnot.

## Struktura kódu

### Třída Game (`Game.xaml.cs`)

#### Pole

- `Score`: Uchovává aktuální skóre hry.
- `TilesCount`: Sleduje počet dlaždic na mřížce.
- `Rand`: Generátor náhodných čísel pro generování dlaždic.
- `Size`, `Increment`, `MaxNew`, `MinNew`: Konstanty a proměnné související s nastavením hry a generováním dlaždic.
- `Tiles[,], BuffTiles[,]`: Pole tlačítek reprezentující herní dlaždice.
- `VirtualTiles[,]`: Virtuální reprezentace dlaždic pro výpočty podobné umělé inteligenci.

#### Metody

- `InitGame()`: Inicializuje herní mřížku a nastavuje počáteční stav.
- `Game_PreviewKeyDown(object sender, KeyEventArgs e)`: Metoda pro zpracování uživatelského vstupu (šipky) pro pohyb dlaždic.
- `TheRest()`, `CountScore()`: Asynchronní metody pro opožděné operace (generování dlaždic a počítání skóre).
- `SpawnTiles()`: Náhodně generuje nové dlaždice na herní mřížce.
- `Move(int DirectionX, int DirectionY)`, `MergeAll(int DirectionX, int DirectionY)`: Metody pro pohyb a slučování dlaždic v daných směrech.
- `RemoveTile(int x, int y)`, `DeleteAll()`: Pomocné metody pro odstraňování dlaždic.
- `SetColor(int x, int y)`, `IncreasedByTimes(int number)`: Metody pro nastavení barev dlaždic podle jejich hodnot.
- `NextStep(object sender, RoutedEventArgs e)`, `MoveVirtual(int DirectionX, int DirectionY)`: Metody související s výpočty podobnými umělé inteligenci pro automatické tahy.

## Použití

1. **Inicializace**: Inicializace třídy `Game`, která nastavuje herní mřížku a spouští herní smyčku.
2. **Hraní**: Použití šipek pro pohyb dlaždic. Dlaždice se slučují, pokud mají stejnou hodnotu a jsou posunuty směrem k sobě.
3. **Skórování**: Skóre se aktualizuje s každým pohybem a slučováním dlaždic.



Poznámka: 
Omlouvám se, neodhadl jsem správně časovou vytíženost svého harmonogramu, takže jsem nestihl zdaleka tolik co jsem chtěl. Jak jistě uvidíte, je zde pokus o automatické jakž takž inteligentní vyplňování a taky přispůsobení hry na začátku v souboru Game.xml, ale ani jedno není  dotaženo jelikož mojí chybou čas tlačí. A ano, dokumentace je vygenerována umělou inteligencí, protože jsem myslel že má mít daleko menší rozsah a můj popis byl poněkud skromný, takže mi přišlo lepší mít to od AI než moc krátké.

