CREATE UNIQUE NONCLUSTERED INDEX [IX_Potwierdzenia] ON [dbo].[Potwierdzenia]
(
	[id_wydania] ASC,
	[id_pracownika] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER VIEW [dbo].[v_wydania]
AS
SELECT Wydania.id, Wydania.Data, Wydania.Ilosc, Towary.id as id_towaru, 
CASE WHEN Premium = 1 THEN Towary.marka + ' ' + Towary.model ELSE Towary.nazwa END as nazwa_towaru,
wydajacy.id as id_wydajacego, ISNULL(Wydajacy.Numer, 'Magazyn') as wydajacy, 
przyjmujacy.id as id_przyjmujacego, ISNULL(Przyjmujacy.Numer, 'Magazyn') as przyjmujacy,
potw_przyjmujacy.data as data_potwierdzenia_przyjecia, potw_wydajacy.data as data_potwierdzenia_wydania
FROM Wydania
JOIN Towary ON Wydania.id_towaru = Towary.Id
LEFT JOIN Pracownicy przyjmujacy ON Wydania.id_przyjmujacego = przyjmujacy.id
LEFT JOIN Pracownicy wydajacy ON Wydania.id_wydajacego = wydajacy.id
LEFT JOIN Potwierdzenia potw_wydajacy ON Wydania.id = potw_wydajacy.id_wydania AND (Wydania.id_wydajacego = potw_wydajacy.id_pracownika OR Wydania.id_wydajacego IS NULL)
LEFT JOIN Potwierdzenia potw_przyjmujacy ON Wydania.id = potw_przyjmujacy.id_wydania AND (Wydania.id_przyjmujacego = potw_przyjmujacy.id_pracownika OR Wydania.id_przyjmujacego IS NULL)
WHERE Wydania.nieaktywne = 0
GO



