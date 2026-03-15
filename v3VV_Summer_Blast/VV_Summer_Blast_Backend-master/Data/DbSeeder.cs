using System;
using System.Collections.Generic;
using System.Linq;
using VVSummerBlastBackendAPI.Models;

namespace VVSummerBlastBackendAPI.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        

        // 1. SZÍNPADOK
        if (!context.Szinpadok.Any())
        {
            context.Szinpadok.AddRange(new List<Szinpad> {
                new Szinpad { Id = 1, Nev = "Vasvilla Main Stage", Helyszin = "Iskolaudvar" },
                new Szinpad { Id = 2, Nev = "Koli Garden Stage", Helyszin = "Kollégiumi kert" },
                new Szinpad { Id = 3, Nev = "Techno Pince", Helyszin = "Alagsori mûhely" }
            });
            context.SaveChanges();
        }

        // 2. ELÕADÓK
        if (!context.Eloadok.Any())
        {
            context.Eloadok.AddRange(new List<Eloado> {
                new Eloado { Id = 1, Nev = "Majka", Leiras = "Híres magyar rapper", KepUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRLtHUK7by-1KvdD-M32TOK0ly5dM3qQLLKBrmGAUTONo7lC3PGWuy16ijlzMw40tQB9hoqLAPAaUnhMzY1xQ17k0tx6sO6B-hzlYLtG9WZ&s=10", Mufaj = "rap", Bio = "Majka az egyik legismertebb magyar rapper és médiaszereplõ.", SpotifyUrl = "https://open.spotify.com/artist/0D8reSG6hzc5KEQWZPYGFB" },
                new Eloado { Id = 2, Nev = "KeineMusik", Leiras = "Világhírû DJ és producer csapat", KepUrl = "https://electronicgroove.com/wp-content/uploads/2024/07/Keinemusik.jpg", Mufaj = "elektronikus", Bio = "A Keinemusik egy 2009-ben, Berlinben alapított elektronikus zenei kollektíva.", SpotifyUrl = "https://open.spotify.com/artist/26WKgv73kRHD0gEDKD1i8j" },
                new Eloado { Id = 3, Nev = "Krúbi", Leiras = "Magyar rapper ironikus szövegekkel", KepUrl = "https://s.24.hu/app/uploads/2025/01/central-0797651545-e1735920033192-1024x576.jpg", Mufaj = "rap", Bio = "Krúbi a kortárs magyar popkultúra egyik legmeghatározóbb elõadója.", SpotifyUrl = "https://open.spotify.com/artist/6u7q0ZGK0oilVYx4kqIk3E" },
                new Eloado { Id = 4, Nev = "Halott Pénz", Leiras = "Népszerû pop-rap együttes", KepUrl = "https://halottpenz.hu/themes/halottpenz/img/hpOG-v1.jpg", Mufaj = "pop", Bio = "A Halott Pénz az egyik legnépszerûbb magyar formáció.", SpotifyUrl = "https://open.spotify.com/artist/0Hir00e5sSBEH8WqOmvi8h" },
                new Eloado { Id = 5, Nev = "Follow The Flow", Leiras = "Háromtagú magyar formáció", KepUrl = "https://i.scdn.co/image/ab6761610000e5ebc069eb6009b4df1c182e692e", Mufaj = "pop", Bio = "A Follow The Flow a hiphop és az élõ hangszeres zene ötvözésével vált ismertté.", SpotifyUrl = "https://open.spotify.com/artist/1UIPahyz7pEKaU6RQvU3FC" },
                new Eloado { Id = 6, Nev = "Wellhello", Leiras = "Dallamos pop duó", KepUrl = "https://lh3.googleusercontent.com/o63164C0_KBPCUOI_Y4H0swsrbMJiPzhnmR_ZoEXJTdmtaHtWkk5y94_NC01rLj5zgkxRV3OvbqlCQ=w544-h544-p-l90-rj", Mufaj = "pop", Bio = "A Wellhello a modern magyar popzene egyik legsikeresebb formációja.", SpotifyUrl = "https://open.spotify.com/artist/4URlJ0EofF5gUvCGDtxr3m" },
                new Eloado { Id = 7, Nev = "Bagossy Brothers Company", Leiras = "Erdélyi magyar zenekar", KepUrl = "https://www.a38.hu/storage/app/uploads/public/5ad/d96/535/thumb_101628_1024_576_0_0_auto.jpg", Mufaj = "rock", Bio = "2013-ban, Gyergyószentmiklóson alakult indie-folk és pop-rock együttes.", SpotifyUrl = "https://open.spotify.com/artist/1vpC76RZf9ndFkOEB7agn9" },
                new Eloado { Id = 8, Nev = "Carson Coma", Leiras = "Alternatív indie rock banda", KepUrl = "https://forbes.hu/extra/30per30-2022/img/data/carson-coma.jpg", Mufaj = "indie", Bio = "A Carson Coma a kortárs magyar alter-pop szcéna legnépszerûbb formációja.", SpotifyUrl = "https://open.spotify.com/artist/1q7g5SBAxtjizS3Vcof6Y6" },
                new Eloado { Id = 9, Nev = "Quimby", Leiras = "Kultikus magyar alternatív zenekar", KepUrl = "https://www.a38.hu/storage/app/uploads/public/5d8/a63/749/thumb_114203_1200_0_0_0_auto.jpg", Mufaj = "alternativ", Bio = "A Quimby a magyar alternatív rockzene egyik legfontosabb zenekara.", SpotifyUrl = "https://open.spotify.com/artist/3ZPq4IH5ZDJKwx5dBQQhBO" },
                new Eloado { Id = 10, Nev = "Kowalsky meg a Vega", Leiras = "Magyar rockzenekar", KepUrl = "https://koncertsziget.hu/concert_admin/images/performers/120/582.jpg", Mufaj = "rock", Bio = "A rock, a pop és a funk elemeit ötvözik egyedi hangzásvilággá.", SpotifyUrl = "https://open.spotify.com/artist/5cllnH1YfZ7XL3mvvgWcGm" },
                new Eloado { Id = 11, Nev = "Tankcsapda", Leiras = "Híres magyar rock banda", KepUrl = "https://pb2.jegy.hu/imgs/system-4/program/000/080/082/tankcsapda-koncert-original-94166.jpg", Mufaj = "rock", Bio = "Magyarország legnépszerûbb rockzenekara 1989 óta.", SpotifyUrl = "https://open.spotify.com/artist/3QbmGjx9VYiu3fpG8NjIZI" },
                new Eloado { Id = 12, Nev = "Mollywood", Leiras = "Feltörekvõ modern pop projekt", KepUrl = "https://images.genius.com/a5c1d9db6a3b5338d0f8df670a560c15.1000x1000x1.jpg", Mufaj = "pop", Bio = "A hazai újhullámos trap színtér egyik meghatározó hangja.", SpotifyUrl = "https://open.spotify.com/artist/4JUoajUgI0mmICMBpV6iEP" },
                new Eloado { Id = 14, Nev = "ByeAlex", Leiras = "Alternatív pop elõadó", KepUrl = "https://cdn2.rtl.hu/55/8f/szakitott-baratnojevel-byealex-vajon-kihez-kerul-macska-haziallat_image_d69c0d392fb1fe5fc54af685acd1?size=w1440", Mufaj = "pop", Bio = "Az ország egyik legmeghatározóbb könnyûzenei figurája.", SpotifyUrl = "https://open.spotify.com/artist/5yfu9LXNlb3GvbZGROjmAj" },
                new Eloado { Id = 15, Nev = "Dzsudló", Leiras = "Modern pop elõadó", KepUrl = "https://i.scdn.co/image/ab6761610000e5eb20f975578f5643d0e524766b", Mufaj = "pop", Bio = "A kortárs magyar pop-trap szcéna egyik legmeghatározóbb alakja.", SpotifyUrl = "https://open.spotify.com/artist/3PMRY3PR5xAe5UpRfPPuaG" },
                new Eloado { Id = 16, Nev = "Analog Balaton", Leiras = "Elektronikus zenei duó", KepUrl = "https://analogbalaton.hu/assets/images/20220218-szyf-29-1004x669.jpeg", Mufaj = "elektronikus", Bio = "A pop, a techno és az indie stílusjegyeit ötvözik.", SpotifyUrl = "https://open.spotify.com/artist/0IUu1vSCfCZJrLHUOgHNWJ" },
                new Eloado { Id = 17, Nev = "Blahalouisiana", Leiras = "Indie pop zenekar", KepUrl = "https://minio.durerkert.com/durer/xxl_BL_4_e26ac855ee.jpg", Mufaj = "indie", Bio = "A 60-as évek beatzenéjét és a modern indie-popot ötvözik.", SpotifyUrl = "https://open.spotify.com/artist/0W2LWS5PPbVl0f6prrMcoP" },
                new Eloado { Id = 18, Nev = "Valmar", Leiras = "Fiatal pop duó", KepUrl = "https://telekom-spots-prod.s3.eu-central-1.amazonaws.com/valmar_1080x867_432650dbb8.png", Mufaj = "pop", Bio = "Valkusz Milán és Marics Peti alkotja a sikeres duót.", SpotifyUrl = "https://open.spotify.com/artist/0EQbKeNi7GXjfN2LndmReh" },
                new Eloado { Id = 19, Nev = "Esti Kornél", Leiras = "Alternatív rock zenekar", KepUrl = "https://veszprembalaton2023.hu/blob/esti-kornel.inbox800x1064.jpg", Mufaj = "rock", Bio = "A dühös gitárcentrikus hangzást mély szövegvilággal ötvözik.", SpotifyUrl = "https://open.spotify.com/artist/6CKGpi9bhf2T7bapMfXl5m" },
                new Eloado { Id = 20, Nev = "Belga", Leiras = "Humoros hip-hop csapat", KepUrl = "https://cdn.origo.hu/2023/12/dlbAvSd0snESexDjKJaBkXchG-uj1t7WmGNJmpUGP7U/fit/1200/800/no/1/aHR0cHM6Ly9jbXNjZG4uYXBwLmNvbnRlbnQucHJpdmF0ZS9jb250ZW50L2QxNDVlYzNlNGQ3YTQ0ZTg4ZDcxMzE4YmZjM2YyMzk0.jpg", Mufaj = "hiphop", Bio = "Ironikus stílusukról ismert formáció.", SpotifyUrl = "https://open.spotify.com/artist/7HhjcR2DpkXelO9wMrmrir" },
                new Eloado { Id = 21, Nev = "Beton.Hofi", Leiras = "Modern underground rapper", KepUrl = "https://kep.cdn.indexvas.hu/1/0/6457/64576/645763/64576365_d759676b5c89ff9bb5ff4fcb3bf2cef0_wm.jpg", Mufaj = "rap", Bio = "A kortárs magyar hiphop egyik legeredetibb hangja.", SpotifyUrl = "https://open.spotify.com/artist/5x9gQC3VztdH5mQO5EEi9y" },
                new Eloado { Id = 22, Nev = "DESH", Leiras = "Feltörekvõ trap elõadó", KepUrl = "https://cdn.forbes.hu/uploads/2025/05/desh-e1748604622108.webp?r=eyJ3IjoyMDQ4LCJxIjo5MCwicyI6ImpwZyJ9", Mufaj = "rap", Bio = "Egyedi, keleti motívumokkal fûszerezett hangzás.", SpotifyUrl = "https://open.spotify.com/artist/4dqqy9z09htrVsRiJpoQmw" },
                new Eloado { Id = 23, Nev = "T.Danny", Leiras = "Népszerû fiatal elõadó", KepUrl = "https://nepszava.hu/i/16/9/1/1562049.jpg", Mufaj = "pop", Bio = "Magabiztos stílus és fülbemászó dallamok.", SpotifyUrl = "https://open.spotify.com/artist/3RDALl5RyRDHPryF1uyWwG" },
                new Eloado { Id = 24, Nev = "Bruno x Spacc", Leiras = "Dinamikus rap duó", KepUrl = "https://media.bigcitylife.hu/media/upload/event/2025/1/event-12469_b.webp", Mufaj = "rap", Bio = "Lendületes és bulizós dalaikkal váltak ismertté.", SpotifyUrl = "https://open.spotify.com/artist/5ALUgNuS421MZrrrAhM9Bv" },
                new Eloado { Id = 25, Nev = "Azahriah", Leiras = "Fiatal magyar énekes és producer", KepUrl = "https://yt3.googleusercontent.com/fKXf-tosyQFlZfAu_9wEYGj9EP-I4zNY1lLwOPdyZJt19or0Oi_u_9gNDmHK46d_ShM8gVZ-Xg=s900-c-k-c0x00ffffff-no-rj", Mufaj = "pop", Bio = "Napjaink legsikeresebb magyar elõadója.", SpotifyUrl = "https://open.spotify.com/artist/6EIriUxo7vznEgJtTDlXpq" },
                new Eloado { Id = 26, Nev = "Akkezdet Phiai", Leiras = "Underground rap", KepUrl = "https://assets.telex.hu/images/20250430/1746017880-temp-MkdIGB_hero-full:xl.jpg", Mufaj = "Rap", Bio = "A magyar underground hiphop kultikus duója.", SpotifyUrl = "https://open.spotify.com/artist/54ARPDPoNgxDl5NbPzjQMh" }
            });
            context.SaveChanges();
        }

        // 3. ESEMÉNYEK
        if (!context.Esemenyek.Any())
        {
            context.Esemenyek.AddRange(new List<Esemeny> {
                new Esemeny { Id = 1, EloadoId = 1, SzinpadId = 1, Kezdes = DateTime.Parse("2026-07-02 16:00:00"), Vege = DateTime.Parse("2026-07-02 18:00:00"), Leiras = "Napi nyitó koncert" },
                new Esemeny { Id = 2, EloadoId = 2, SzinpadId = 2, Kezdes = DateTime.Parse("2026-07-02 16:00:00"), Vege = DateTime.Parse("2026-07-02 18:00:00"), Leiras = "Délutáni pörgés" },
                new Esemeny { Id = 3, EloadoId = 3, SzinpadId = 3, Kezdes = DateTime.Parse("2026-07-02 16:00:00"), Vege = DateTime.Parse("2026-07-02 18:00:00"), Leiras = "Alagsori hangulat" },
                new Esemeny { Id = 4, EloadoId = 4, SzinpadId = 1, Kezdes = DateTime.Parse("2026-07-02 18:00:00"), Vege = DateTime.Parse("2026-07-02 20:00:00"), Leiras = "Naplemente szett" },
                new Esemeny { Id = 5, EloadoId = 5, SzinpadId = 2, Kezdes = DateTime.Parse("2026-07-02 18:00:00"), Vege = DateTime.Parse("2026-07-02 20:00:00"), Leiras = "Esti alapozó" },
                new Esemeny { Id = 6, EloadoId = 6, SzinpadId = 3, Kezdes = DateTime.Parse("2026-07-02 18:00:00"), Vege = DateTime.Parse("2026-07-02 20:00:00"), Leiras = "Közönségkedvenc" },
                new Esemeny { Id = 7, EloadoId = 7, SzinpadId = 1, Kezdes = DateTime.Parse("2026-07-02 20:00:00"), Vege = DateTime.Parse("2026-07-02 22:00:00"), Leiras = "Main Stage Headline" },
                new Esemeny { Id = 8, EloadoId = 8, SzinpadId = 2, Kezdes = DateTime.Parse("2026-07-02 20:00:00"), Vege = DateTime.Parse("2026-07-02 22:00:00"), Leiras = "Kerti fõmûsor" },
                new Esemeny { Id = 9, EloadoId = 9, SzinpadId = 3, Kezdes = DateTime.Parse("2026-07-02 20:00:00"), Vege = DateTime.Parse("2026-07-02 22:00:00"), Leiras = "Techno zárás" },
                new Esemeny { Id = 14, EloadoId = 14, SzinpadId = 2, Kezdes = DateTime.Parse("2026-07-03 18:00:00"), Vege = DateTime.Parse("2026-07-03 20:00:00"), Leiras = "Alternatív est" },
                new Esemeny { Id = 15, EloadoId = 15, SzinpadId = 3, Kezdes = DateTime.Parse("2026-07-03 18:00:00"), Vege = DateTime.Parse("2026-07-03 20:00:00"), Leiras = "Pince buli" },
                new Esemeny { Id = 16, EloadoId = 16, SzinpadId = 1, Kezdes = DateTime.Parse("2026-07-03 20:00:00"), Vege = DateTime.Parse("2026-07-03 22:00:00"), Leiras = "Elektronikus orgia" },
                new Esemeny { Id = 17, EloadoId = 17, SzinpadId = 2, Kezdes = DateTime.Parse("2026-07-03 20:00:00"), Vege = DateTime.Parse("2026-07-03 22:00:00"), Leiras = "Indie vibe" },
                new Esemeny { Id = 18, EloadoId = 18, SzinpadId = 3, Kezdes = DateTime.Parse("2026-07-03 20:00:00"), Vege = DateTime.Parse("2026-07-03 22:00:00"), Leiras = "Pop-rap parádé" },
                new Esemeny { Id = 19, EloadoId = 19, SzinpadId = 1, Kezdes = DateTime.Parse("2026-07-04 16:00:00"), Vege = DateTime.Parse("2026-07-04 18:00:00"), Leiras = "Zárónapi nyitó" },
                new Esemeny { Id = 20, EloadoId = 20, SzinpadId = 2, Kezdes = DateTime.Parse("2026-07-04 16:00:00"), Vege = DateTime.Parse("2026-07-04 18:00:00"), Leiras = "Klasszikus magyar hiphop" },
                new Esemeny { Id = 21, EloadoId = 21, SzinpadId = 3, Kezdes = DateTime.Parse("2026-07-04 16:00:00"), Vege = DateTime.Parse("2026-07-04 18:00:00"), Leiras = "Modern flow" },
                new Esemeny { Id = 22, EloadoId = 22, SzinpadId = 1, Kezdes = DateTime.Parse("2026-07-04 18:00:00"), Vege = DateTime.Parse("2026-07-04 20:00:00"), Leiras = "Trap bomba" },
                new Esemeny { Id = 23, EloadoId = 23, SzinpadId = 2, Kezdes = DateTime.Parse("2026-07-04 18:00:00"), Vege = DateTime.Parse("2026-07-04 20:00:00"), Leiras = "Fiatalok kedvence" },
                new Esemeny { Id = 24, EloadoId = 24, SzinpadId = 3, Kezdes = DateTime.Parse("2026-07-04 18:00:00"), Vege = DateTime.Parse("2026-07-04 20:00:00"), Leiras = "Duó show" },
                new Esemeny { Id = 25, EloadoId = 25, SzinpadId = 1, Kezdes = DateTime.Parse("2026-07-04 20:00:00"), Vege = DateTime.Parse("2026-07-04 22:00:00"), Leiras = "FESZTIVÁLZÁRÓ KONCERT" }
            });
            context.SaveChanges();
        }

        // 4. TERMÉKEK (Merch + Teljes Jegy/Bérlet kínálat, immár Készlettel kiegészítve)
        if (!context.Termekek.Any())
        {
            context.Termekek.AddRange(new List<Termek> {
                // Merch ruházat
                new Termek { Id = 1, Nev = "VVSB Retro 24 fekete póló", Ar = 8990.00m, Tipus = "ruhazat", KepUrl = "vv_merch_fekete_polo_1.png", Keszlet = 50 },
                new Termek { Id = 2, Nev = "Vasvilla Summer Blast fekete póló #2", Ar = 8990.00m, Tipus = "ruhazat", KepUrl = "vv_merch_fekete_polo_2.png", Keszlet = 50 },
                new Termek { Id = 3, Nev = "Vasvilla Summer Blast fekete póló #3", Ar = 8990.00m, Tipus = "ruhazat", KepUrl = "vv_merch_fekete_polo_3.png", Keszlet = 50 },
                new Termek { Id = 4, Nev = "Vasvilla Summer Blast fekete póló #4", Ar = 8990.00m, Tipus = "ruhazat", KepUrl = "vv_merch_fekete_polo_4.png", Keszlet = 50 },
                new Termek { Id = 5, Nev = "VVSB Retro 24 fehér póló", Ar = 8990.00m, Tipus = "ruhazat", KepUrl = "vv_merch_feher_polo_1.png", Keszlet = 50 },
                new Termek { Id = 6, Nev = "Vasvilla Summer Blast kapucnis pulóver", Ar = 13990.00m, Tipus = "ruhazat", KepUrl = "vv_merch_fekete_pulcsi_1.png", Keszlet = 50 },
                // Kiegészítõk
                new Termek { Id = 7, Nev = "Vasvilla Summer Blast vászontáska", Ar = 3990.00m, Tipus = "kiegeszito", KepUrl = "vv_merch_vaszontaska_1.png", Keszlet = 50 },
                new Termek { Id = 8, Nev = "Vasvilla Summer Blast kulacs", Ar = 4990.00m, Tipus = "kiegeszito", KepUrl = "vv_merch_kulacs_1.png", Keszlet = 50 },
                new Termek { Id = 9, Nev = "Vasvilla Summer Blast baseball sapka", Ar = 5990.00m, Tipus = "kiegeszito", KepUrl = "vv_merch_sapka_1.png", Keszlet = 50 },
                new Termek { Id = 10, Nev = "Vasvilla Summer Blast repohár", Ar = 2490.00m, Tipus = "kiegeszito", KepUrl = "vv_merch_repohar_1.png", Keszlet = 50 },

                // JEGYEK (KÉSZLETEN)
                new Termek { Id = 11, Nev = "Napijegy - Csütörtök", Ar = 16990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 500 },
                new Termek { Id = 12, Nev = "Napijegy - Péntek", Ar = 21990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 500 },
                new Termek { Id = 13, Nev = "Napijegy - Szombat", Ar = 26990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 500 },
                new Termek { Id = 14, Nev = "VIP napijegy - Csütörtök", Ar = 29990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 150 },
                new Termek { Id = 15, Nev = "VIP napijegy - Péntek", Ar = 35990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 150 },
                new Termek { Id = 16, Nev = "VIP napijegy - Szombat", Ar = 39990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 150 },
                new Termek { Id = 17, Nev = "Diák napijegy - Csütörtök", Ar = 9990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 500 },
                new Termek { Id = 18, Nev = "Diák napijegy - Péntek", Ar = 12990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 500 },
                new Termek { Id = 19, Nev = "Diák napijegy - Szombat", Ar = 13990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 500 },
                
                // BÉRLETEK (KÉSZLETEN)
                new Termek { Id = 20, Nev = "3 napos Bérlet", Ar = 45990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 500 },
                new Termek { Id = 21, Nev = "3 napos diák Bérlet", Ar = 41900.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 500 },
                new Termek { Id = 22, Nev = "3 napos tanár Bérlet", Ar = 35990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 200 },
                new Termek { Id = 23, Nev = "3 napos VIP Bérlet", Ar = 79990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 150 },
                
                // KEMPING ÉS SZÁLLÁS JEGYEK (KÉSZLETEN)
                new Termek { Id = 24, Nev = "Telepített Sátor Bérlet (2 fõre)", Ar = 26990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 100 },
                new Termek { Id = 25, Nev = "Komfort Igloo Bérlet (2 fõre)", Ar = 94990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 50 },
                new Termek { Id = 26, Nev = "Igloo Box Bérlet (2 fõre)", Ar = 134990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 30 },
                new Termek { Id = 27, Nev = "Telepített Sátor Bérlet (3 fõre)", Ar = 40990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 100 },
                new Termek { Id = 28, Nev = "Deluxe Igloo Bérlet (3 fõre)", Ar = 119990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 50 },
                new Termek { Id = 29, Nev = "Deluxe Igloo Bérlet (4 fõre)", Ar = 134990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 50 },
                new Termek { Id = 30, Nev = "Karaván Parcella", Ar = 24990.00m, Tipus = "Jegy", KepUrl = "", Keszlet = 100 }
            });
            context.SaveChanges();
        }

        // 5. HOSTELEK
        if (!context.Hostelek.Any())
        {
            context.Hostelek.AddRange(new List<Hostel> {
                new Hostel { Id = 1, Nev = "Botanikus Kerti Diákhotel", Ar = 14788, KepUrl = "https://www.sopronfestszallas.hu/static/images/98PlOTgIDdIwM/img-7367.jpg", Leiras = "A Diákszálló a Soproni Egyetem botanikus kertjénél található.", Link = "http://uf.nyme.hu/szallas_old/web_tartalom.php?menu_id=104&username=&lang=hun" },
                new Hostel { Id = 2, Nev = "Hotel Palatinus****", Ar = 29700, KepUrl = "https://www.sopronfestszallas.hu/static/images/EA9axTaIN0Few/dsc03046.jpg", Leiras = "Sopron történelmi belvárosában, a fesztiváltól 10 percre.", Link = "https://www.palatinussopron.com/" },
                new Hostel { Id = 3, Nev = "Hotel Civitas****", Ar = 32950, KepUrl = "https://www.sopronfestszallas.hu/static/images/WXKawSoIkVUE0/dsc03413-k.jpg", Leiras = "Sopron belvárosának szívében, prémium kényelemmel.", Link = "https://civitashotel.com/" },
                new Hostel { Id = 4, Nev = "Jégverem Fogadó", Ar = 16900, KepUrl = "https://www.sopronfestszallas.hu/static/images/XQlxxi0IwNfeN/dsc-8185-k.jpg", Leiras = "Klasszikus fogadó, bõséges reggelivel és hangulatos szobákkal.", Link = "https://szallas.hu/jegverem-fogado-sopron" }
            });
            context.SaveChanges();
        }

        // 6. KEMPINGEK
        if (!context.Kempingek.Any())
        {
            context.Kempingek.AddRange(new List<Kemping> {
                new Kemping { Id = 1, Nev = "Telepített Sátor 2 fõre", Ar = 26990, KepUrl = "https://sopronfest.hu/wp-content/uploads/2024/02/Telepi%CC%81tett-Sa%CC%81tor-2-fo%CC%8Bre-ku%CC%88lso%CC%8B.jpg", Leiras = "Elõre felállított sátor, matraccal és világítással." },
                new Kemping { Id = 2, Nev = "Komfort Igloo 2 fõre", Ar = 94990, KepUrl = "https://sopronfest.hu/wp-content/uploads/2024/02/Komfort-Igloo-2-fo%CC%8Bre-k-768x490.jpg", Leiras = "Zárható, árammal ellátott kemping egység." },
                new Kemping { Id = 3, Nev = "Igloo Box 2 fõre", Ar = 134990, KepUrl = "https://sopronfest.hu/wp-content/uploads/2024/02/Igloo-Box-2-fo%CC%8Bre-ku%CC%88lso%CC%8B.jpg", Leiras = "Prémium fesztivál szállás saját ágynemûvel és kényelemmel." },
                new Kemping { Id = 7, Nev = "Karaván Parcella", Ar = 24900, KepUrl = "https://cdn.pixabay.com/photo/2016/11/21/14/31/caravan-1845719_1280.jpg", Leiras = "Hely saját lakókocsi vagy lakóautó számára áramcsatlakozással." }
            });
            context.SaveChanges();
        }
    }
}