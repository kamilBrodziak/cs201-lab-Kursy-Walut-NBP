# Kursy walut z NBP

* Autor: _Krzysztof Molenda_
* wersja: 1.0 (2020.01.05)

## Wprowadzenie

Narodowy Bank Polski (NBP) archiwizuje kursy walut oraz cen złota w formie plików `.xml` i udostępnia je publicznie

* za pomocą NBP Web API (<http://api.nbp.pl/>)
* za pośrednictwem strony <http://www.nbp.pl/kursy/xml/>

Szczegółowa instrukcja pobierania kursów walut oraz sposobu kodowania plików znajduje się na stronie: <https://www.nbp.pl/home.aspx?f=/kursy/instrukcja_pobierania_kursow_walut.html>

UWAGA: Poniższe zadanie wykonujesz **nie wykorzystując** NBP Web API.

## Zadanie

Napisz program pobierający stosowne pliki ze strony NBP i, dla kursu **kupna** oraz kursu **sprzedaży** określonej waluty w podanym przedziale czasowym, wyświetlający podstawowe statystyki - oddzielnie dla kursu kupna i kursu sprzedaży:

* kurs średni (średnia arytmetyczna)
* odchylenie standardowe (_Standard Deviation_)
* kurs minimalny
* kurs maksymalny

oraz

* datę(y) i wartość największej (największych - jeśli będzie więcej) różnicy kursowej.

Bierzesz pod uwagę kursy następujących walut: USD, EUR, CHF, GBP.
Podany masz przedział czasowy.

**Wejście:**

* kod waluty (USD, EUR, CHF, GBP)
* przedział czasu dla jakiego zostaną pobrane dane – data początkowa i data końcowa

**Wyjście:**

* kurs średni (średnia arytmetyczna)
* odchylenie standardowe
* kurs minimalny
* kurs maksymalny

oraz

* datę(y) i wartość największej (największych - jeśli będzie więcej) różnic kursowych.

Sposób formatowania wyników - dowolny.

Aplikację zrealizuj w dwóch wariantach:

1. jako aplikację konsolową, wsadową, gdzie parametry uruchomienia przekażesz w linii komend, np.:

   ```c:\>Waluty.exe EUR 2018-09-01 2018-09-20```

    a wyniki wypiszesz na standardowe wyjście (konsolę),

2. jako prostą aplikację jednookienkową (WPF), gdzie parametry i wyniki pojawią się w polach formularza.

Aplikacja nie powinna zwracać wyjątków - obsłuż te, które mogą się pojawić.

## Cel ćwiczenia

* pobranie pliku tekstowego z zasobu sieciowego i jego analiza
* pobranie plików XML z zasobów sieciowych, ich parsowanie i przetwarzanie
* obsługa wyjątków

## Podpowiedzi

* Aby pobrać pliki z Internetu prawdopodobnie skorzystasz z klasy [WebClient](https://docs.microsoft.com/pl-pl/dotnet/api/system.net.webclient?view=netstandard-2.1) (lub, bardziej surowej [HttpWebResponse](https://docs.microsoft.com/pl-pl/dotnet/api/system.net.httpwebresponse?view=netstandard-2.1)) - oraz stosownych metod pobierających pliki (być może asynchronicznie).
* Pliki XML możesz ładować prostszymi metodami:
  * <https://support.microsoft.com/en-ie/help/307643/how-to-read-xml-data-from-a-url-by-using-visual-c>
  * <https://docs.microsoft.com/pl-pl/dotnet/api/system.xml.xmldocument?view=netstandard-2.1>
  * <https://lonewolfonline.net/introduction-xml/>
* Parsowanie plików XML możesz zrealizować na wiele różnych sposób:, np. używając [LinqToXML](https://docs.microsoft.com/pl-pl/dotnet/csharp/programming-guide/concepts/linq/linq-to-xml-overview), [XMLDocument](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xmldocument?redirectedfrom=MSDN&view=netframework-4.8), [XPath](https://docs.microsoft.com/en-us/dotnet/api/system.xml.xpath?redirectedfrom=MSDN&view=netframework-4.8) czy via [XML serialisation](https://docs.microsoft.com/en-us/dotnet/standard/serialization/introducing-xml-serialization).