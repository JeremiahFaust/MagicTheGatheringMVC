using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MagicDbContext;
using MagicDbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicRepository
{
    public class MagicCardRepository
    {
        MagicContext ctxt;

        public MagicCardRepository(MagicContext context)
        {
            this.ctxt = context;

        }

        #region Deep Query Methods

        public IQueryable<Card> DeepQueryCard()
        {
            return ctxt.Cards
                .Include(c => c.CardTypes)
                    .ThenInclude(ct => ct.Type)
                .Include(c => c.Rulings)
                .Include(c => c.CardAbilities)
                    .ThenInclude(ca => ca.Ability)
                .Include(c => c.ManaCosts)
                    .ThenInclude(mc => mc.Color);
        }

        public IQueryable<MultiverseCard> DeepQueryMultiverse()
        {
            return ctxt.MultiverseCards
                .Include(mc => mc.cards)
                    .ThenInclude(c => c.CardTypes)
                        .ThenInclude(ct => ct.Type)
                .Include(mc => mc.cards)
                    .ThenInclude(c => c.Rulings)
                .Include(mc => mc.cards)
                    .ThenInclude(c => c.CardAbilities)
                        .ThenInclude(ca => ca.Ability)
                 .Include(mc => mc.cards)
                    .ThenInclude(c => c.ManaCosts)
                        .ThenInclude(m => m.Color)
                 .Include(c => c.Set);
        }

        public IQueryable<ManaCosts> DeepQueryManaCosts()
        {
            return ctxt.ManaCosts
                .Include(mc => mc.Color);
        }

        public IQueryable<CardTypes> DeepQueryCardTypes()
        {
            return ctxt.CardTypes
                .Include(ct => ct.Type);
        }

        public IQueryable<CardAbilities> DeepQueryCardAbilities()
        {
            return ctxt.CardAbilities
                .Include(ca => ca.Ability);
        }

        #endregion

        #region Card Methods

        public async Task<Card> GetCardAsync(string multiverseID, string cardNumber)
        {
            return await Task.Run(() => (GetCard(multiverseID, cardNumber)));
        }

        public Card GetCard(string multiverseID, string cardNumber)
        {
            if (multiverseID == null || cardNumber == null) throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(multiverseID) || string.IsNullOrWhiteSpace(cardNumber)) return null;
            if (!ctxt.Cards.Any()) throw new Exception("No Cards Found!");
            Card c = DeepQueryCard().SingleOrDefault(ca =>  ca.MultiverseID.Equals(multiverseID) && ca.CardNumber.Equals(cardNumber));
            return c;
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await Task.Run(() => (GetAllCards()));
        }

        public IEnumerable<Card> GetAllCards()
        {
            IEnumerable<Card> cards;
            
            var car = DeepQueryCard();
            cards = car.ToList();
            if (cards == null || cards.Count() == 0) return null;
            return cards;

        }

        public async Task<IEnumerable<Card>> GetAllCardsByArtistAsync(string artistName)
        {
            return await Task.Run(() => (GetAllCardsByArtist(artistName)));
        }

        public IEnumerable<Card> GetAllCardsByArtist(string artistName)
        {
            if (artistName == null) throw new ArgumentNullException();
            IEnumerable<Card> cards;

            cards =DeepQueryCard().Where<Card>(w => w.Artist.Contains(artistName));

            if (cards == null || cards.Count() == 0) return null;
            return cards;
        }

        public async Task<IEnumerable<Card>> GetAllCardsByCardNameAsync(string cardName)
        {
            return await Task.Run(() => (GetAllCardsByCardName(cardName)));
        }

        public IEnumerable<Card> GetAllCardsByCardName(string cardName)//should get all cards that contain cardName
        {
            if (cardName == null) throw new ArgumentNullException();
            IEnumerable<Card> cards;
            
            cards = DeepQueryCard().Where(c => c.CardName.Contains(cardName));

            if (cards ==null || cards.Count() == 0) return null;
            return cards;
        }

        public async Task<IEnumerable<Card>> GetAllCArdsByConvertedManaCostAsync(int manaCost)
        {
            return await Task.Run(() => (GetAllCArdsByConvertedManaCost(manaCost)));
        }

        public IEnumerable<Card> GetAllCArdsByConvertedManaCost(int manaCost)
        {
            IEnumerable<Card> cards;

            cards = DeepQueryCard().Where<Card>(c => c.ConvertedManaCost == manaCost);

            if (cards == null || cards.Count() == 0) return null;
            return cards;
        }

        public async Task<IEnumerable<Card>> GetAllCardsByManaColorsAsync(string[] colorSymbols)
        {
            return await Task.Run(() => (GetAllCardsByManaColors(colorSymbols)));
        }

        public IEnumerable<Card> GetAllCardsByManaColors(string[] colorSymbols)
        {
            if ((colorSymbols == null) || (colorSymbols.Length == 0)) throw new ArgumentNullException();
            IEnumerable<Card> cards;

            cards =DeepQueryCard().Where<Card>(c => c.ManaCosts.Where<ManaCosts>(mc => colorSymbols.Contains(mc.Color.Symbol)).Any());
            
            if (cards == null || cards.Count() == 0) return null;
            return cards;
        }

        public async Task<IEnumerable<Card>> GetAllCardsByPowerAsync(int power)
        {
            return await Task.Run(() => GetAllCardsByPower(power));
        }

        public IEnumerable<Card> GetAllCardsByPower(int power)
        {
            IEnumerable<Card> cards;

            cards = DeepQueryCard().Where(c=> c.Power == power);

            if (cards == null || cards.Count() == 0) return null;
            return cards;
        }

        public async Task<IEnumerable<Card>> GetAllCardsByToughnessAsync(int toughness)
        {
            return await Task.Run(() => GetAllCardsByToughness(toughness));
        }

        public IEnumerable<Card> GetAllCardsByToughness(int toughness)
        {
            IEnumerable<Card> cards;

            cards = DeepQueryCard().Where(c=>c.Toughness==toughness);
            
            if (cards == null || cards.Count() == 0) return null;
            return cards;
        }

        public async Task<IEnumerable<Card>> GetAllCardsByTypeAsync(string type)
        {
            return await Task.Run(() => GetAllCardsByType(type));
        }

        public IEnumerable<Card> GetAllCardsByType(string type)
        {
            if (type == null) throw new ArgumentNullException();
            IEnumerable<Card> cards;

            cards = DeepQueryCard().Where(c => c.CardTypes.Any(ct => ct.Type.Name.Contains(type))).ToList();

            //cards = new List<Card>();
            //cards.AddRange(DeepQueryCard().SelectMany(c => c.CardTypes).Select(t => t.Card));

            //foreach (Card c in DeepQueryCard())
            //{
            //    foreach (CardTypes ct in c.CardTypes)
            //    {
            //        if (ct.Type.Name.Contains(type, StringComparison.OrdinalIgnoreCase)) { cards.Add(c); break; }
            //    }
            //}
            if (cards == null || cards.Count() == 0) return null;
            return cards;
        }

        public async Task<IEnumerable<Card>> GetAllCardsByAbilityAsync(string ability)
        {
            return await Task.Run(() => GetAllCardsByAbility(ability));
        }

        public IEnumerable<Card> GetAllCardsByAbility(string ability)//should get cards that contain ability
        {
            if(ability == null)throw new ArgumentNullException();
            IEnumerable<Card> cards;


            cards = DeepQueryCard().Where(c => c.CardAbilities.Any(ct => ct.Ability.Ability.Contains(ability)));

            //cards = new List<Card>();
            //foreach (Card c in DeepQueryCard())
            //{
            //    foreach (CardAbilities ct in c.CardAbilities)
            //    {
            //        if (ct.Ability.Ability.Contains(ability, StringComparison.OrdinalIgnoreCase)) { cards.Add(c); break; }
            //    }
            //}
            if (cards == null || cards.Count() == 0) return null;
            return cards;
        }

        #endregion

        #region Set Methods

        public async Task<Sets> GetSetAsync(string setAbbr)
        {
            return await Task.Run(() => (GetSet(setAbbr)));
        }

        public Sets GetSet(string setAbbr)
        {
            if (setAbbr == null) throw new ArgumentNullException();

            Sets set = ctxt.Sets.SingleOrDefault(s => s.SetAbbr.Equals(setAbbr));
            
            return set;
        }
        
        public async Task<IEnumerable<Sets>> GetAllSetsAsync()
        {
            return await Task.Run(() => (GetAllSets()));
        }

        public IEnumerable<Sets> GetAllSets()
        {
            IEnumerable<Sets> sets = ctxt.Sets.ToArray();
            if (sets == null || sets.Count() == 0) return null;
            return sets;
        }

        #endregion

        #region Rulings Methods
        
        public async Task<IEnumerable<Rulings>> GetRulingsAsync(string multiverseId, string cardNumber)
        {
            return await Task.Run(() => (GetRulings(multiverseId, cardNumber)));
        }

        public IEnumerable<Rulings> GetRulings(string multiverseId, string cardNumber)
        {
            if (multiverseId == null || cardNumber == null) throw new ArgumentNullException();
            IEnumerable<Rulings> rulings = ctxt.Rulings.Where(r => r.CardID.Equals(multiverseId) && r.CardNumber.Equals(cardNumber));

            if (rulings == null || rulings.Count() == 0) return null;
            return rulings;
        }
        
        public async Task<IEnumerable<Rulings>> GetAllRulingsAsync()
        {
            return await Task.Run(() => (GetAllRulings()));
        }

        public IEnumerable<Rulings> GetAllRulings()
        {
            IEnumerable<Rulings> rulings = ctxt.Rulings.ToArray();

            if (rulings == null || rulings.Count() == 0) return null;
            return rulings;
        }

        #endregion

        #region Type Methods

        public async Task<Types> GetTypeAsync(int typeId)
        {
            return await Task.Run(() => (GetType(typeId)));
        }

        public Types GetType(int typeId)
        {
            Types types = ctxt.Types.SingleOrDefault(t=>t.ID == typeId);
            return types;
        }

        public async Task<IEnumerable<Types>> GetAllTypeAsync()
        {
            return await Task.Run(() => (GetAllTypes()));
        }

        public IEnumerable<Types> GetAllTypes()
        {
            IEnumerable<Types> types = ctxt.Types.ToArray();

            if (types == null || types.Count() == 0) return null;
            return types;
        }

        #endregion  

        #region MultiverseCard Methods

        public async Task<MultiverseCard> GetMultiverseCardAsync(string multiverseId)
        {
            return await Task.Run(() => (GetMultiverseCard(multiverseId)));
        }

        public MultiverseCard GetMultiverseCard(string multiverseId)
        {
            if (multiverseId == null) throw new ArgumentNullException();
            MultiverseCard mc = DeepQueryMultiverse().SingleOrDefault(m => m.MultiverseId.Equals(multiverseId));
            return mc;
        }

        public async Task<IEnumerable<MultiverseCard>> GetAllMultiverseCardsAsync()
        {
            return await Task.Run(() => (GetAllMultiverseCards()));
        }

        public IEnumerable<MultiverseCard> GetAllMultiverseCards()
        {
            IEnumerable<MultiverseCard> mcards = DeepQueryMultiverse().ToArray();

            if (mcards == null || mcards.Count() == 0) return null;
            return mcards;
        }

        public async Task<IEnumerable<MultiverseCard>> GetAllMultiverseCardsBySetAsync(string setAbbr)
        {
            return await Task.Run(() => (GetAllMultiverseCardsBySet(setAbbr)));
        }

        public IEnumerable<MultiverseCard> GetAllMultiverseCardsBySet(string setAbbr)
        {
            if (setAbbr == null) throw new ArgumentNullException();
            if (string.IsNullOrEmpty(setAbbr)) return null;
            IEnumerable<MultiverseCard> mcards = DeepQueryMultiverse().Where(m => m.SetID.Contains(setAbbr));

            if (mcards == null || mcards.Count() == 0) return null;
            return mcards;
        }

        #endregion

        #region ManaCosts Methods

        public async Task<IEnumerable<ManaCosts>> GetManaCostAsync(string multiverseId, string cardnumber)
        {
            return await Task.Run(() => (GetManaCost(multiverseId, cardnumber)));
        }

        public IEnumerable<ManaCosts> GetManaCost(string multiverseId, string cardnumber)
        {
            if (multiverseId == null || cardnumber == null) throw new ArgumentNullException();
            IEnumerable<ManaCosts> mc = DeepQueryManaCosts().Where(m=>m.CardID.Equals(multiverseId) && m.CardNumber.Equals(cardnumber));

            if (mc == null || mc.Count() == 0) return null;
            return mc;
        }

        #endregion

        #region Color Methods

        public async Task<Color> GetColorAsync(int colorId)
        {
            return await Task.Run(() => (GetColor(colorId)));
        }

        public Color GetColor(int colorId)
        {
            Color c = ctxt.Color.SingleOrDefault(co=> co.ID==colorId);
            return c;
        }

        public async Task<IEnumerable<Color>> GetAllColorsAsync()
        {
            return await Task.Run(() => (GetAllColors()));
        }

        public IEnumerable<Color> GetAllColors()
        {
            IEnumerable<Color> colors = ctxt.Color.ToArray();
            if (colors == null || colors.Count() == 0) return null;
            return colors;
        }

        #endregion

        #region CardTypes Methods

        public async Task<IEnumerable<CardTypes>> GetCardTypesAsync(string multiverseId, string cardNumber)
        {
            return await Task.Run(() => (GetCardTypes(multiverseId, cardNumber)));
        }

        public IEnumerable<CardTypes> GetCardTypes(string multiverseId, string cardNumber)
        {
            if (multiverseId == null || cardNumber == null) throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(multiverseId) || string.IsNullOrWhiteSpace(cardNumber)) return null;
            IEnumerable<CardTypes> ctypes = DeepQueryCardTypes().Where(c=>c.CardID.Equals(multiverseId) && c.CardNumber.Equals(cardNumber));

            if (ctypes == null || ctypes.Count() == 0) return null;
            return ctypes;
        }

        #endregion

        #region CardAbilities Methods

        public async Task<IEnumerable<CardAbilities>> GetCardAbilitesAsync(string multiverseId, string cardNumber)
        {
            return await Task.Run(() => (GetCardAbilites(multiverseId, cardNumber)));
        }

        public IEnumerable<CardAbilities> GetCardAbilites(string multiverseId, string cardNumber)
        {
            if (multiverseId == null || cardNumber == null) throw new ArgumentNullException();
            IEnumerable<CardAbilities> cAbilities = DeepQueryCardAbilities().Where(ca=> ca.CardID.Equals(multiverseId) && ca.CardNumber.Equals(cardNumber));

            if (cAbilities == null || cAbilities.Count() == 0) return null;
            return cAbilities;

        }

        #endregion

        #region Ability Methods

        public async Task<Abilities> GetAbilityAsync(int abilityId)
        {
            return await Task.Run(() => (GetAbility(abilityId)));
        }

        public Abilities GetAbility(int abilityId)
        {
            Abilities ab = ctxt.Abilities.SingleOrDefault(a=> a.AbilityID==abilityId);
            return ab;
        }

        public async Task<IEnumerable<Abilities>> getAllAbilitiesAsync()
        {
            return await Task.Run(() => (getAllAbilities()));
        }

        public IEnumerable<Abilities> getAllAbilities()
        {
            IEnumerable<Abilities> abilities = ctxt.Abilities.ToArray();

            if (abilities == null || abilities.Count() == 0) return null;
            return abilities;
        }

        #endregion

    }

    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }
    }
}
