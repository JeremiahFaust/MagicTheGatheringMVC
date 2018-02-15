using MagicDbContext;
using MagicDbContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MagicRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MTGMVCTestProject
{
    [TestClass]
    public class MagicRepositoryTest
    {
        MagicCardRepository rep;

        public MagicRepositoryTest()
        {
            DbContextOptionsBuilder<MagicContext> dbContextOptions = new DbContextOptionsBuilder<MagicContext>();
            
            dbContextOptions.EnableSensitiveDataLogging();
            dbContextOptions.UseSqlite(@"Data Source=C:\Users\jd\source\repos\MTGMVC\MTGMVC\MagicDB.db", providerOptions => providerOptions.CommandTimeout(60));

            this.rep = new MagicCardRepository(new MagicContext(dbContextOptions.Options));

        }

        #region getCardTests

        /// <summary>
        /// tests that two cards are equal
        /// </summary>
        [TestMethod]
        public async Task GetCardTest1()
        {
            Card c = await rep.GetCardAsync("423808", "141");
            Card c1 = new Card { MultiverseID = "423808", CardNumber = "141", Artist= "Kieran Yanner", FlavorText= "#_The streets of Ghirapur have become dangerous. It's good to have a dependable companion._#", CardName= "Aegis Automaton", HighPrice=0.95, MidPrice=0.1, LowPrice=0.01, ConvertedManaCost=2, IsDualCard=false, Power=0, Toughness=3, Rarity="C", Rating=5 };

            Assert.AreEqual(c1,c);
        }

        /// <summary>
        /// tests that two cards are equal
        /// </summary>
        [TestMethod]
        public async Task GetCardTest2()
        {
            Card c = await rep.GetCardAsync("25928", "110");
            Card c1 = new Card { MultiverseID = "25928", CardNumber = "110", Artist = "Michael Sutfin", FlavorText = "#_\"That didn't happen naturally,\" said Multani. \"Rath's overlay is interfering with the Æther itself.\"_#", CardName = "Horned Kavu", HighPrice = 0.94, MidPrice = 0.18, LowPrice = 0.08, ConvertedManaCost = 2, IsDualCard = false, Power = 3, Toughness = 4, Rarity = "C", Rating = 0 };

            Assert.AreEqual(c1, c);
        }

        /// <summary>
        /// tests that two cards are equal
        /// </summary>
        [TestMethod]
        public async Task GetCardTest3()
        {
            Card c = await rep.GetCardAsync("426914", "212b");
            Card c1 = new Card { MultiverseID = "426914", CardNumber = "212b", Artist = "Daarken", FlavorText = "", CardName = "Return", HighPrice = 4.5, MidPrice = 1.11, LowPrice = 0.7, ConvertedManaCost = 4, IsDualCard = true, Power = 0, Toughness = 0, Rarity = "R", Rating = 5 };

            Assert.AreEqual(c1, c);
        }

        /// <summary>
        /// tests that two cards are equal
        /// </summary>
        [TestMethod]
        public async Task GetCardTest4()
        {
            Card c = await rep.GetCardAsync("423808", "141");
            Card c1 = new Card { MultiverseID = "423808", CardNumber = "141", Artist = "Kieran Yanner", FlavorText = "#_The streets of Ghirapur have become dangerous. It's good to have a dependable companion._#", CardName = "Aegis Automaton", HighPrice = 0.95, MidPrice = 0.1, LowPrice = 0.01, ConvertedManaCost = 2, IsDualCard = false, Power = 0, Toughness = 3, Rarity = "C", Rating = 5 };

            Assert.AreEqual(c1, c);
        }

        /// <summary>
        /// tests that two cards are not equal
        /// </summary>
        [TestMethod]
        public async Task GetCardTest5()
        {
            Card c = await rep.GetCardAsync("426914", "212b");
            Card c1 = new Card { MultiverseID = "423808", CardNumber = "141", Artist = "Kieran Yanner", FlavorText = "#_The streets of Ghirapur have become dangerous. It's good to have a dependable companion._#", CardName = "Aegis Automaton", HighPrice = 0.95, MidPrice = 0.1, LowPrice = 0.01, ConvertedManaCost = 2, IsDualCard = false, Power = 0, Toughness = 3, Rarity = "C", Rating = 5 };

            Assert.AreNotEqual(c1, c);
        }

        /// <summary>
        /// tests that two cards are not equal
        /// </summary>
        [TestMethod]
        public async Task GetCardTest6()
        {
            Card c = await rep.GetCardAsync("12691", "120");
            Card c1 = new Card { MultiverseID = "423808", CardNumber = "141", Artist = "Kieran Yanner", FlavorText = "#_The streets of Ghirapur have become dangerous. It's good to have a dependable companion._#", CardName = "Aegis Automaton", HighPrice = 0.95, MidPrice = 0.1, LowPrice = 0.01, ConvertedManaCost = 2, IsDualCard = false, Power = 0, Toughness = 3, Rarity = "C", Rating = 5 };

            Assert.AreNotEqual(c1, c);
        }

        /// <summary>
        /// tests that two cards are not equal
        /// </summary>
        [TestMethod]
        public async Task GetCardTest7()
        {
            Card c = await rep.GetCardAsync("25913", "35");
            Card c1 = new Card { MultiverseID = "423808", CardNumber = "141", Artist = "Kieran Yanner", FlavorText = "#_The streets of Ghirapur have become dangerous. It's good to have a dependable companion._#", CardName = "Aegis Automaton", HighPrice = 0.95, MidPrice = 0.1, LowPrice = 0.01, ConvertedManaCost = 2, IsDualCard = false, Power = 0, Toughness = 3, Rarity = "C", Rating = 5 };

            Assert.AreNotEqual(c1, c);
        }

        /// <summary>
        /// tests that null is returned if a card is not found
        /// </summary>
        [TestMethod]
        public async Task GetCardTest8()
        {
            Card c = await rep.GetCardAsync("423808", "110");

            Assert.AreEqual(null, c);
        }

        /// <summary>
        /// tests that exception is thrown if first parameter is null
        /// </summary>
        [TestMethod]
        public async Task GetCardTest9()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => rep.GetCardAsync(null, "110"));
        }

        /// <summary>
        /// tests that exception is thrown if second parameter is null
        /// </summary>
        [TestMethod]
        public async Task GetCardTest10()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => rep.GetCardAsync("423808", null));
        }

        /// <summary>
        /// tests that exception is thrown if either parameter is null
        /// </summary>
        [TestMethod]
        public async Task GetCardTest11()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(()=> rep.GetCardAsync(null,null));
        }

        /// <summary>
        /// tests that null is returned if either parameter is empty
        /// </summary>
        [TestMethod]
        public async Task GetCardTest12()
        {
            Card c = await rep.GetCardAsync("", "");

            Assert.AreEqual(null, c);
        }

        /// <summary>
        /// verifies that all cards are returned
        /// </summary>
        [TestMethod]
        public async Task GetAllCardsTest()
        {
            IEnumerable<Card> c = await rep.GetAllCardsAsync();
            Assert.AreEqual(36302, c.Count());
        }

        /// <summary>
        /// tests that all cards drawn by same artist are returned
        /// </summary>
        [TestMethod]
        public async Task GetAllCardsByArtistTest()
        {
            IEnumerable<Card> c = await rep.GetAllCardsByArtistAsync("Kieran Yanner");
            foreach(Card k in c)
            {
                if (k.Artist.Equals("Kieran Yanner")) continue;
                else
                    Assert.Fail();
            }
            Assert.AreEqual(0, 0);
        }

        /// <summary>
        /// tests that all returned cards contain the passed in card name
        /// </summary>
        [TestMethod]
        public async Task GetAllCardsByCardNameTest()
        {
            IEnumerable<Card> ca = await rep.GetAllCardsByCardNameAsync("Heroic Defiance");
            foreach(Card c in ca)
            {
                if (c.CardName.Contains("Heroic Defiance")) continue;
                else
                    Assert.Fail();
            }
            Assert.AreEqual(0,0);
        }

        /// <summary>
        /// tests that all returned Cards have the correct ManaCost
        /// </summary>
        [TestMethod]
        public async Task GetAllCardsByConvertedManaCostTest()
        {
            IEnumerable<Card> ca = await rep.GetAllCArdsByConvertedManaCostAsync(1);
            foreach(Card c in ca)
            {
                if (c.ConvertedManaCost == 1) continue;
                else
                    Assert.Fail();
            }
            Assert.AreEqual(0, 0);
        }

        /// <summary>
        /// tests that all returned cards have the appropriate colors
        /// </summary>
        [TestMethod]
        public async Task GetAllCardsByManaColorsTest ()
        {
            IEnumerable<Card> ca = await rep.GetAllCardsByManaColorsAsync(new string[] { "U", "B" });
            foreach(Card c in ca)
            {
                bool valid = false;
                foreach(ManaCosts mc in c.ManaCosts)
                {
                    if (mc.Color.Symbol.Equals("U") || mc.Color.Symbol.Equals("B")) { valid = true; break; }
                }
                if (!valid) Assert.Fail();
            }
            Assert.AreEqual(0, 0);
        }

        /// <summary>
        /// tests all returned cards have passed in power
        /// </summary>
        [TestMethod]
        public async Task GetAllCardsByPowerTest()
        {
            IEnumerable<Card> ca = await rep.GetAllCardsByPowerAsync(1);
            foreach(Card c in ca)
            {
                if (c.Power == 1) continue;
                else
                    Assert.Fail();
            }
            Assert.AreEqual(0, 0);
        }

        /// <summary>
        /// tests all returned cards have passed in toughness
        /// </summary>
        [TestMethod]
        public async Task GetAllCardsByToughnessTest()
        {
            IEnumerable<Card> ca = await rep.GetAllCardsByToughnessAsync(1);
            foreach (Card c in ca)
            {
                if (c.Toughness == 1) continue;
                else
                    Assert.Fail();
            }
            Assert.AreEqual(0, 0);
        }

        /// <summary>
        /// tests that all returned cards have the correct type
        /// </summary>
        [TestMethod]
        public async Task GetAllCardsByTypeTest()
        {
            IEnumerable<Card> ca = await rep.GetAllCardsByTypeAsync("Artifact");
            foreach(Card c in ca)
            {
                bool valid = false;
                foreach(CardTypes ct in c.CardTypes)
                {
                    if (ct.Type.Name.Contains("Artifact", StringComparison.OrdinalIgnoreCase)) { valid = true; break; }
                }
                if (!valid) Assert.Fail();
            }
            Assert.AreEqual(0, 0);
        }

        /// <summary>
        /// tests that all returned cards contain the requested ability
        /// </summary>
        [TestMethod]
        public async Task GetAllCardsByAbilityTest()
        {
            IEnumerable<Card> ca = await rep.GetAllCardsByAbilityAsync("Flying");
            foreach(Card c in ca)
            {
                bool valid = false;
                foreach(CardAbilities a in c.CardAbilities)
                {
                    if (a.Ability.Ability.Contains("Flying", StringComparison.OrdinalIgnoreCase))
                    {
                        valid = true;
                        break;
                    }
                }
                if (!valid)
                {
                    Assert.Fail();
                }
            }
            Assert.AreEqual(0, 0);
        }

        #endregion

        #region getSetTests

        /// <summary>
        /// tests that the correct set is grabbed from the database
        /// </summary>
        [TestMethod]
        public async Task GetSetTest1()
        {
            Sets s = await rep.GetSetAsync("AER");
            Sets s1 = new Sets { SetAbbr = "AER", SetFullName = "Aether Revolt" };
            Assert.AreEqual(s1, s);
        }

        /// <summary>
        /// tests that the correct set is grabbed from the database
        /// </summary>
        [TestMethod]
        public async Task GetSetTest2()
        {
            Sets s = await rep.GetSetAsync("ROE");
            Sets s1 = new Sets { SetAbbr = "ROE", SetFullName = "Rise of the Eldrazi" };
            Assert.AreEqual(s1, s);
        }

        /// <summary>
        /// tests that the correct set is grabbed from the database where the full set name is not set
        /// </summary>
        [TestMethod]
        public async Task GetSetTest3()
        {
            Sets s = await rep.GetSetAsync("UGIN");
            Sets s1 = new Sets { SetAbbr = "UGIN", SetFullName = "" };
            Assert.AreEqual(s1, s);
        }

        /// <summary>
        /// tests that null is returned when invalid setABBR is passed in
        /// </summary>
        [TestMethod]
        public async Task GetSetTest4()
        {
            Sets s = await rep.GetSetAsync("QWERTY");
            Assert.AreEqual(null, s);
        }

        /// <summary>
        /// tests that null is returned when an empty string is passed in
        /// </summary>
        [TestMethod]
        public async Task GetSetTest5()
        {
            Sets s = await rep.GetSetAsync("");
            Assert.AreEqual(null, s);
        }

        /// <summary>
        /// tests that an exception is thrown when null is passed in
        /// </summary>
        [TestMethod]
        public async Task GetSetTest6()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(()=>rep.GetSetAsync(null));
        }

        /// <summary>
        /// test that all sets are returned
        /// </summary>
        [TestMethod]
        public async Task GetAllSetsTest()
        {
            IEnumerable<Sets> s = await rep.GetAllSetsAsync();
            Assert.AreEqual(224, s.Count());
        }

        #endregion

        #region getRulingsTests

        /// <summary>
        /// test that the expected number of rulings are returned
        /// </summary>
        [TestMethod]
        public async Task GetRulingsTest1()
        {
            IEnumerable<Rulings> r = await rep.GetRulingsAsync("SUS_6", "6");
            Assert.AreEqual(4, r.Count());
        }

        /// <summary>
        /// test that null is returned if a card has no rulings
        /// </summary>
        [TestMethod]
        public async Task GetRulingsTest2()
        {
            IEnumerable<Rulings> r = await rep.GetRulingsAsync("1", "230");
            Assert.AreEqual(null, r);
        }

        /// <summary>
        /// test that null is returned if invalid paramaters are passed
        /// </summary>
        [TestMethod]
        public async Task GetRulingsTest3()
        {
            IEnumerable<Rulings> r = await rep.GetRulingsAsync("qwertry", "qwertyu");
            Assert.AreEqual(null, r);
        }

        /// <summary>
        /// test that Exception is thrown in null is passed as a parameter
        /// </summary>
        [TestMethod]
        public async Task GetRulingsTest4()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(()=> rep.GetRulingsAsync(null, null));
        }

        /// <summary>
        /// tests that all returned rulings belong to correct card
        /// </summary>
        [TestMethod]
        public async Task GetRulingsTest5()
        {
            IEnumerable<Rulings> r = await rep.GetRulingsAsync("SUS_6", "6");
            foreach(Rulings ru in r)
            {
                if (ru.CardID.Equals("SUS_6") && ru.CardNumber.Equals("6")) continue;
                else
                    Assert.Fail();
            }
            Assert.AreEqual(0, 0);
        }

        /// <summary>
        /// tests that all rulings are returned
        /// </summary>
        [TestMethod]
        public async Task GetAllRulingsTest()
        {
            IEnumerable<Rulings> r = await rep.GetAllRulingsAsync();
            Assert.AreEqual(2072, r.Count());
        }

        #endregion

        #region getTypesTests

        /// <summary>
        /// tests that the correct type is returned
        /// </summary>
        [TestMethod]
        public async Task GetTypeTest1()
        {
            Types t = await rep.GetTypeAsync(1);
            Types t1 = new Types { ID = 1, Name="Artifact" };
            Assert.AreEqual(t1,t);
        }

        /// <summary>
        /// tests that the correct type is returned
        /// </summary>
        [TestMethod]
        public async Task GetTypeTest2()
        {
            Types t = await rep.GetTypeAsync(10);
            Types t1 = new Types { ID = 10, Name = "Domri" };
            Assert.AreEqual(t1, t);
        }

        /// <summary>
        /// tests that the correct type is returned
        /// </summary>
        [TestMethod]
        public async Task GetTypeTest3()
        {
            Types t = await rep.GetTypeAsync(100);
            Types t1 = new Types { ID = 100, Name = "Jace" };
            Assert.AreEqual(t1, t);
        }

        /// <summary>
        /// tests that null is returned if invalid value if returned
        /// </summary>
        [TestMethod]
        public async Task GetTypeTest4()
        {
            Types t = await rep.GetTypeAsync(int.MaxValue);
            Assert.AreEqual(null, t);
        }

        /// <summary>
        /// tests that null is returned if invalid value if returned
        /// </summary>
        [TestMethod]
        public async Task GetTypeTest5()
        {
            Types t = await rep.GetTypeAsync(0);
            Assert.AreEqual(null, t);
        }

        /// <summary>
        /// tests that null is returned if invalid value if returned
        /// </summary>
        [TestMethod]
        public async Task GetTypeTest6()
        {
            Types t = await rep.GetTypeAsync(410);
            Assert.AreEqual(null, t);
        }

        /// <summary>
        /// tests that null is returned if invalid value if returned
        /// </summary>
        [TestMethod]
        public async Task GetTypeTest7()
        {
            Types t = await rep.GetTypeAsync(-1);
            Assert.AreEqual(null, t);
        }

        /// <summary>
        /// tests that null is returned if invalid value if returned
        /// </summary>
        [TestMethod]
        public async Task GetAllTypesTest1()
        {
            IEnumerable<Types> t = await rep.GetAllTypeAsync();
            Assert.AreEqual(409, t.Count());
        }


        #endregion

        #region getMultiverseCardTests

        /// <summary>
        /// tests that the correct multiverse card is returned
        /// </summary>
        [TestMethod]
        public async Task GetMultiverseCardTest1()
        {
            MultiverseCard mc = await rep.GetMultiverseCardAsync("423808");
            MultiverseCard mc1 = new MultiverseCard { MultiverseId = "423808", ImagePath = @"AER\423808.full.jpg", SetID = "AER" };

            Assert.AreEqual(mc1, mc);
        }

        /// <summary>
        /// tests that the correct multiverse card is returned
        /// </summary>
        [TestMethod]
        public async Task GetMultiverseCardTest2()
        {
            MultiverseCard mc = await rep.GetMultiverseCardAsync("25872");
            MultiverseCard mc1 = new MultiverseCard { MultiverseId = "25872", ImagePath = @"PLS\25872.full.jpg", SetID = "PLS" };

            Assert.AreEqual(mc1, mc);
        }

        /// <summary>
        /// tests that the correct multiverse card is returned
        /// </summary>
        [TestMethod]
        public async Task GetMultiverseCardTest3()
        {
            MultiverseCard mc = await rep.GetMultiverseCardAsync("SF-PXTC_250b");
            MultiverseCard mc1 = new MultiverseCard { MultiverseId = "SF-PXTC_250b", ImagePath = @"PXTC\SF-PXTC_250b.full.jpg", SetID = "PXTC" };

            Assert.AreEqual(mc1, mc);
        }

        /// <summary>
        /// tests that null is returned when invalid data is passed
        /// </summary>
        [TestMethod]
        public async Task GetMultiverseCardTest4()
        {
            MultiverseCard mc = await rep.GetMultiverseCardAsync("");

            Assert.AreEqual(null, mc);
        }

        /// <summary>
        /// tests that null is returned when invalid data is passed
        /// </summary>
        [TestMethod]
        public async Task GetMultiverseCardTest5()
        {
            MultiverseCard mc = await rep.GetMultiverseCardAsync("qwerty");

            Assert.AreEqual(null, mc);
        }

        /// <summary>
        /// tests that null is returned when invalid data is passed
        /// </summary>
        [TestMethod]
        public async Task GetMultiverseCardTest6()
        {
            MultiverseCard mc = await rep.GetMultiverseCardAsync("-1");

            Assert.AreEqual(null, mc);
        }

        /// <summary>
        /// tests that null is returned when invalid data is passed
        /// </summary>
        [TestMethod]
        public async Task GetMultiverseCardTest7()
        {
            MultiverseCard mc = await rep.GetMultiverseCardAsync(" ");

            Assert.AreEqual(null, mc);
        }

        /// <summary>
        /// tests that exception is thrown when null is passed
        /// </summary>
        [TestMethod]
        public async Task GetMultiverseCardTest8()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(()=> rep.GetMultiverseCardAsync(null));
        }

        /// <summary>
        /// tests that all multiverse cards are returned
        /// </summary>
        [TestMethod]
        public async Task GetAllMultiverseCardsTest()
        {
            IEnumerable<MultiverseCard> mc = await rep.GetAllMultiverseCardsAsync();
            Assert.AreEqual(36203, mc.Count());
        }

        /// <summary>
        /// tests that all multiverse cards by set are retrieved
        /// </summary>
        [TestMethod]
        public async Task GetAllMultiverseCardsBySetTest()
        {
            IEnumerable<MultiverseCard> mc = await rep.GetAllMultiverseCardsBySetAsync("AER");

            Assert.AreEqual(198,mc.Count());
        }

        /// <summary>
        /// tests that null is returned if invalid set id is passed
        /// </summary>
        [TestMethod]
        public async Task GetAllMultiverseCardsBySetTest3()
        {
            IEnumerable<MultiverseCard> mc = await rep.GetAllMultiverseCardsBySetAsync("");

            Assert.AreEqual(null, mc);
        }

        /// <summary>
        /// tests that exception is thrown if null is passed
        /// </summary>
        [TestMethod]
        public async Task GetAllMultiverseCardsBySetTest4()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(()=> rep.GetAllMultiverseCardsBySetAsync(null));
        }

        /// <summary>
        /// tests that all multiverse cards by set are retrieved
        /// </summary>
        [TestMethod]
        public async Task GetAllMultiverseCardsBySetTest1()
        {
            IEnumerable<MultiverseCard> mc = await rep.GetAllMultiverseCardsBySetAsync("AER");

            foreach(MultiverseCard m in mc)
            {
                if (m.SetID.Equals("AER")) continue;
                else
                    Assert.Fail();
            }

            Assert.AreEqual(0, 0);
        }

        /// <summary>
        /// tests that all multiverse cards by set are retrieved
        /// </summary>
        [TestMethod]
        public async Task GetAllMultiverseCardsBySetTest2()
        {
            IEnumerable<MultiverseCard> mc = await rep.GetAllMultiverseCardsBySetAsync("PLS");

            foreach (MultiverseCard m in mc)
            {
                if (m.SetID.Equals("PLS")) continue;
                else
                    Assert.Fail();
            }

            Assert.AreEqual(0, 0);
        }

        #endregion

        #region getManaCostTests

        /// <summary>
        /// checks that the correct number of mana costs are returned for given card
        /// </summary>
        [TestMethod]
        public async Task GetManaCostTest1()
        {
            IEnumerable<ManaCosts> mc = await rep.GetManaCostAsync("39711", "22");

            Assert.AreEqual(2, mc.Count());
        }

        /// <summary>
        /// tests that all returned ManaCosts belong to the correct card
        /// </summary>
        [TestMethod]
        public async Task GetManaCostTest4()
        {
            IEnumerable<ManaCosts> ca = await rep.GetManaCostAsync("10520", "95");

            foreach (ManaCosts c in ca)
            {
                if (c.CardID.Equals("10520") && c.CardNumber.Equals("95")) continue;
                else
                    Assert.Fail();

            }
            Assert.AreEqual(0, 0);
        }

        /// <summary>
        /// checks that null is returned if invalid values are passed in
        /// </summary>
        [TestMethod]
        public async Task GetManaCostTest2()
        {
            IEnumerable<ManaCosts> mc = await rep.GetManaCostAsync("39711asdf", "22asdf");

            Assert.AreEqual(null, mc);
        }

        /// <summary>
        /// checks that exception is thrown if null values are passed
        /// </summary>
        [TestMethod]
        public async Task GetManaCostTest3()
        {
           await Assert.ThrowsExceptionAsync<ArgumentNullException>(()=> rep.GetManaCostAsync(null, null));
           
        }

        #endregion

        #region getColorTests

        /// <summary>
        /// tests that the correct color is returned
        /// </summary>
        [TestMethod]
        public async Task GetColorTest1()
        {
            Color c = await rep.GetColorAsync(1);
            Color c1 = new Color { ID = 1, Name = "Uncolored", Symbol = "2" };

            Assert.AreEqual(c1, c);
        }

        /// <summary>
        /// tests that the correct color is returned
        /// </summary>
        [TestMethod]
        public async Task GetColorTest2()
        {
            Color c = await rep.GetColorAsync(10);
            Color c1 = new Color { ID = 10, Name = "Uncolored", Symbol = "13" };

            Assert.AreEqual(c1, c);
        }

        /// <summary>
        /// tests that the correct color is returned
        /// </summary>
        [TestMethod]
        public async Task GetColorTest3()
        {
            Color c = await rep.GetColorAsync(23);
            Color c1 = new Color { ID = 23, Name = "X", Symbol = "X" };

            Assert.AreEqual(c1, c);
        }

        /// <summary>
        /// tests that the correct color is returned
        /// </summary>
        [TestMethod]
        public async Task GetColorTest4()
        {
            Color c = await rep.GetColorAsync(24);
            Color c1 = new Color { ID = 24, Name = "Energy", Symbol = "E" };

            Assert.AreEqual(c1, c);
        }

        /// <summary>
        /// tests that null is returned for invalid input
        /// </summary>
        [TestMethod]
        public async Task GetColorTest5()
        {
            Color c = await rep.GetColorAsync(25);

            Assert.AreEqual(null, c);
        }

        /// <summary>
        /// tests that null is returned for invalid input
        /// </summary>
        [TestMethod]
        public async Task GetColorTest6()
        {
            Color c = await rep.GetColorAsync(0);

            Assert.AreEqual(null, c);
        }

        /// <summary>
        /// tests that null is returned for invalid input
        /// </summary>
        [TestMethod]
        public async Task GetColorTest7()
        {
            Color c = await rep.GetColorAsync(-5);

            Assert.AreEqual(null, c);
        }

        /// <summary>
        /// tests that all colors are successfully loaded
        /// </summary>
        [TestMethod]
        public async Task getAllColors()
        {
            IEnumerable<Color> c = await rep.GetAllColorsAsync();
            Assert.AreEqual(24, c.Count());
        }

        #endregion

        #region getCardTypesTest

        /// <summary>
        /// tests that the correct number of cardtypes are returned
        /// </summary>
        [TestMethod]
        public async Task GetCardTypesTest1()
        {
            IEnumerable<CardTypes> ct = await rep.GetCardTypesAsync("10520", "95");

            Assert.AreEqual(4, ct.Count());
        }

        /// <summary>
        /// tests that all returned Card Types belong to the correct card
        /// </summary>
        [TestMethod]
        public async Task GetCardTypesTest4()
        {
            IEnumerable<CardTypes> ca = await rep.GetCardTypesAsync("10520", "95");

            foreach (CardTypes c in ca)
            {
                if (c.CardID.Equals("10520") && c.CardNumber.Equals("95")) continue;
                else
                    Assert.Fail();

            }
            Assert.AreEqual(0,0);
        }

        /// <summary>
        /// tests that null is returned if no match is found
        /// </summary>
        [TestMethod]
        public async Task GetCardTypesTest2()
        {
            IEnumerable<CardTypes> ct = await rep.GetCardTypesAsync("", "");

            Assert.AreEqual(null, ct);
        }

        /// <summary>
        /// tests that exception is thrown if null is passed in
        /// </summary>
        [TestMethod]
        public async Task GetCardTypesTest3()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(()=> rep.GetCardTypesAsync(null, null));
        }

        #endregion

        #region  getCardAbilitiesTests

        /// <summary>
        /// tests that the correct number of cardabilites are returned
        /// </summary>
        [TestMethod]
        public async Task GetAbilitiesTest1()
        {
            IEnumerable<CardAbilities> ca = await rep.GetCardAbilitesAsync("10520", "95");
            Assert.AreEqual(2, ca.Count());
        }

        /// <summary>
        /// tests that all returned Card Abilities belong to the correct card
        /// </summary>
        [TestMethod]
        public async Task GetAbilitiesTest4()
        {
            IEnumerable<CardAbilities> ca = await rep.GetCardAbilitesAsync("10520", "95");

            foreach(CardAbilities c in ca)
            {
                if (c.CardID.Equals("10520") && c.CardNumber.Equals("95")) continue;
                else
                    Assert.Fail();    

            }

            Assert.AreEqual(0,0);
        }

        /// <summary>
        /// tests that null is returned when an invalid parameter is passed
        /// </summary>
        [TestMethod]
        public async Task GetAbilitiesTest2()
        {
            IEnumerable<CardAbilities> ca = await rep.GetCardAbilitesAsync("", "");
            Assert.AreEqual(null, ca);
        }

        /// <summary>
        /// tests that an exception is thrown if null is passed as a parameter
        /// </summary>
        [TestMethod]
        public async Task GetAbilitiesTest3()
        {
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(()=> rep.GetCardAbilitesAsync(null, null));
        }

        #endregion

        #region getAbilitiesTests

        /// <summary>
        /// Tests that the correct Ability is returned
        /// </summary>
        [TestMethod]
        public async Task GetAbilityTest1()
        {
            Abilities a = await rep.GetAbilityAsync(1);
            Abilities a1 = new Abilities { AbilityID = 1, Ability = "{4}{W}: Return another target creature you control to its owner's hand." };

            Assert.AreEqual(a1, a);
        }

        /// <summary>
        /// Tests that the correct Ability is returned
        /// </summary>
        [TestMethod]
        public async Task GetAbilityTest2()
        {
            Abilities a = await rep.GetAbilityAsync(10);
            Abilities a1 = new Abilities { AbilityID = 10, Ability = "• Search your library for up to two basic land cards, reveal them, put them into your hand, then shuffle your library.", Description = null };

            Assert.AreEqual(a1, a);
        }

        /// <summary>
        /// Tests that the correct Ability is returned
        /// </summary>
        [TestMethod]
        public async Task GetAbilityTest3()
        {
            Abilities a = await rep.GetAbilityAsync(100);
            Abilities a1 = new Abilities { AbilityID = 100, Ability = "Each player may look at and play cards he or she exiled with Shared Fate.", Description = null };

            Assert.AreEqual(a1, a);
        }

        /// <summary>
        /// Tests that the correct Ability is returned
        /// </summary>
        [TestMethod]
        public async Task GetAbilityTest4()
        {
            Abilities a = await rep.GetAbilityAsync(21641);
            Abilities a1 = new Abilities { AbilityID = 21641, Ability = "Each player shuffles his or her hand and graveyard into his or her library, then draws seven cards. #_(Then put Timetwister into its owner's graveyard._#)", Description = null };

            Assert.AreEqual(a1, a);
        }

        /// <summary>
        /// Tests that null is returned for invalid input
        /// </summary>
        [TestMethod]
        public async Task GetAbilityTest5()
        {
            Abilities a = await rep.GetAbilityAsync(21642);
            Assert.AreEqual(null, a);
        }

        /// <summary>
        /// Tests that null is returned for invalid input
        /// </summary>
        [TestMethod]
        public async Task GetAbilityTest6()
        {
            Abilities a = await rep.GetAbilityAsync(0);
            Assert.AreEqual(null, a);
        }

        /// <summary>
        /// Tests that null is returned for invalid input
        /// </summary>
        [TestMethod]
        public async Task GetAbilityTest7()
        {
            Abilities a = await rep.GetAbilityAsync(int.MaxValue);
            Assert.AreEqual(null, a);
        }

        /// <summary>
        /// Tests that null is returned for invalid input
        /// </summary>
        [TestMethod]
        public async Task GetAbilityTest8()
        {
            Abilities a = await rep.GetAbilityAsync(-1);
            Assert.AreEqual(null, a);
        }

        /// <summary>
        /// Tests that null is returned for invalid input
        /// </summary>
        [TestMethod]
        public async Task GetAbilityTest9()
        {
            Abilities a = await rep.GetAbilityAsync(int.MinValue);
            Assert.AreEqual(null, a);
        }

        /// <summary>
        /// gets all abilites
        /// </summary>
        [TestMethod]
        public async Task GetAllAbilities()
        {
            IEnumerable<Abilities> a = await rep.getAllAbilitiesAsync();

            Assert.AreEqual(21641, a.Count());
        }
        
        #endregion

    }
}
