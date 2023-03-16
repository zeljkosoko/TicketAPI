*** WebAPI servis u ASP.NET CORE 3

#1 Aplikacija kod klijenta kreira tiket sa naslovom i opisom, upisuje u njihovu bazu i salje json podatak: 
        "Klijent",
        "Mesto":
        "Adresa"
        "Kreator-ime"
        "Kreator-prezime"
        "IP adresa"
        "ID tiketa" 
        "Broj dokumenta tiketa"
        "Naslov"
        "Opis"
        "Datum kreiranja":
        "Inicijator-ime"
        "Inicijator-prezime"

#2 Postoje dva scenarija na osnovu podataka u json-u: 
        a) kada klijent kreira tiket (Kreator) onda je inicijator je null
        b) kada administrator kreira(Kreator) na zahtev klijentovog zaposlenog (Inicijator)

#3 Servis na osnovu json-a treba da kreira tiket u našoj bazi, koji sadrži sledeća polja:
        KorisnikKlijentMestoAdresaId (Korisnik je ili kreator ili inicijator)
        KorisnikId (Zebracon Solutions)
        TiketId
        BrojDokumenta
        Status
        Naslov
        Opis
        DatumKreiranja
        *ostala polja su null

        THE END ----------------
    