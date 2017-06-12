using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Blackjack
{
    class Program
    {
        static bool cheat = false;
        static bool outOfCards = false;
        static bool stay = true;
        static string input;
        static bool done = false;
        static void Main()
        {
            List<string> freshDeck = new List<string>();
            freshDeck = Deck.CreateDeck();
            Deck.ShuffleDeck();
            do
            {
                Console.WriteLine("New Hand");
                Dealer.DealerFaceDown = " ";
                Dealer.DealerFaceUp.Clear();
                Player.PlayerFaceUp.Clear();
                DealAHand();
                if (!outOfCards)
                {
                    done = false;

                    do
                    {
                        stay = false;
                        DisplayCardsOnTable();
                        do
                        {
                            Console.WriteLine(" ");
                            Console.WriteLine("h enter = take a card");
                            Console.WriteLine("s enter = stay");
                            Console.WriteLine("e enter = exit");
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "h":
                                    DealACard("Player", "up");
                                    break;
                                case "s":
                                    stay = true;
                                    break;
                                case "e":
                                    done = true;
                                    break;
                                case "cheat":
                                    cheat = true;
                                    continue;
                                default:
                                    break;
                            }
                        } while (input != "h" && input != "s" && input != "e");
                        if (Valuation.Value("Player") > 21)
                        {
                            Console.WriteLine("Player:");
                            for (int index = 0; index < Player.PlayerFaceUp.Count; index++)
                            {
                                Console.WriteLine(Player.PlayerFaceUp[index]);
                            }
                            Console.WriteLine("You busted.  Dealer takes your money.");
                            done = true;
                        }
                        if (stay && Valuation.Value("Dealer") > 17 && Valuation.Value("Dealer") <= 21)
                        {
                            if (Valuation.Value("Dealer") > Valuation.Value("Player"))
                            {
                                Console.WriteLine("Dealer wins");
                                Console.WriteLine("Dealer's face down card is " + Dealer.DealerFaceDown + ".");
                            }
                            else
                            {
                                Console.WriteLine("Player wins");
                                Console.WriteLine("Dealer's face down card was " + Dealer.DealerFaceDown);
                            }
                            done = true;
                        }
                        if (Valuation.Value("Dealer") < 18)
                        {
                            Console.WriteLine("Dealer takes a card");
                            DealACard("Dealer", "up");
                            Console.WriteLine("face down card");
                            for (int index = 0; index < Dealer.DealerFaceUp.Count; index++)
                            {
                                Console.WriteLine(Dealer.DealerFaceUp[index]);
                            }
                        }
                        if (Valuation.Value("Dealer") > 21)
                        {
                            Console.WriteLine("Dealer busted.  You win.");
                            Console.WriteLine(Dealer.DealerFaceDown);
                            for (int index = 0; index < Dealer.DealerFaceUp.Count; index++)
                            {
                                Console.WriteLine(Dealer.DealerFaceUp[index]);
                            }
                            done = true;
                        }
                        if (Valuation.Value("Dealer") == 21)
                        {
                            Console.WriteLine("Dealer's face down card is " + Dealer.DealerFaceDown + ".");
                            Console.WriteLine("Dealer wins.");
                            done = true;
                        }
                        if (Valuation.Value("Player") == 21)
                        {
                            Console.WriteLine("Dealer's face down card is " + Dealer.DealerFaceDown + ".");
                            Console.WriteLine("Player wins.");
                            done = true;
                        }
                    } while (!done);
                }
                for (int index = 0; index < 10; index++)
                {
                    Console.WriteLine(" ");
                }
            } while (input != "e");
        }
        class Deck
        {
            static List<string> suits = new List<string>()
            {
            "none",
            "Clubs",
            "Diamonds",
            "Hearts",
            "Spades"
            };
            public static List<string> ShuffledDeck = new List<string>();
            public static List<string> NewDeck = new List<string>();
            static string cardName;
            public static List<string> CreateDeck()
            {
                for (int index = 1; index <= 4; index++)
                {
                    for (int counter = 1; counter <= 13; counter++)
                    {
                        switch (counter)
                        {
                            case 1:
                                cardName = "Ace";
                                break;
                            case 11:
                                cardName = "Jack";
                                break;
                            case 12:
                                cardName = "Queen";
                                break;
                            case 13:
                                cardName = "King";
                                break;
                            default:
                                cardName = counter.ToString();
                                break;
                        }
                        NewDeck.Add(suits[index] + "_" + cardName);
                    }
                }
                return NewDeck;
            }
            public static void ShuffleDeck()
            {
                int elementNumber;
                int numberInDeck = NewDeck.Count;
                for (int index = numberInDeck - 1; index >= 1; index--)
                {
                    elementNumber = RandomGenerator();
                    ShuffledDeck.Add(NewDeck[elementNumber]);
                    NewDeck.Remove(NewDeck[elementNumber]);
                    Deck.Sleep();
                    numberInDeck--;
                }
            }
            public static void Sleep()
            {
                System.Threading.Thread.Sleep(100);
            }
            public static int RandomGenerator()
            {
                Random randomNumber = new Random();
                int outValue = randomNumber.Next(0, NewDeck.Count);
                return outValue;
            }
            public static void RemoveCardFromDeck(List<string> bigList, int indexToRemove)
            {
                bigList.RemoveAt(indexToRemove);
            }
            public static void RemoveCardFromDeck(List<string> bigList, string elementToRemove)
            {
                bigList.Remove(elementToRemove);
            }
        }
        public static void DealAHand()
        {
            if (!outOfCards)
            {
                DealACard("Player", "up");
            }
            if (!outOfCards)
            {
                DealACard("Dealer", "down");
            }
            if (!outOfCards)
            {
                DealACard("Player", "up");
            }
            if (!outOfCards)
            {
                DealACard("Dealer", "up");
            }
        }
        public static void DisplayCardsOnTable()
        {
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("Dealer:");
            if (Program.cheat)
            {
                Console.WriteLine(Dealer.DealerFaceDown);
            }
            else
            {
                Console.WriteLine("face down card");
            }
            for (int index = 0; index < Dealer.DealerFaceUp.Count; index++)
            {
                Console.WriteLine(Dealer.DealerFaceUp[index]);
            }
            Console.WriteLine(" ");
            Console.WriteLine("Player:");
            for (int index = 0; index < Player.PlayerFaceUp.Count; index++)
            {
                Console.WriteLine(Player.PlayerFaceUp[index]);
            }
        }
        public static void DealACard(string recipient, string face)
        {
            if (Deck.ShuffledDeck.Count == 0) 
            {
                Console.WriteLine("Ran out of cards.  Game over");
                Program.done = true;
                Program.outOfCards = true;
                Program.input = "e";
            }
            if (!outOfCards)
            {
                if (recipient == "Player")
                {
                    Player.PlayerFaceUp.Add(Deck.ShuffledDeck[0]);
                    Deck.RemoveCardFromDeck(Deck.ShuffledDeck, 0);
                }
                else
                {
                    if (face == "down")
                    {
                        Dealer.DealerFaceDown = Deck.ShuffledDeck[0];
                        Deck.RemoveCardFromDeck(Deck.ShuffledDeck, 0);
                    }
                    else
                    {
                        Dealer.DealerFaceUp.Add(Deck.ShuffledDeck[0]);
                        Deck.RemoveCardFromDeck(Deck.ShuffledDeck, 0);
                    }
                }
            }
        }
        class Valuation
        {
            public static bool AceHigh = false;
            public static List<string> ValueList = new List<string>();
            public static int Total;
            public static int AceCount = 0;
            public static int Value(string participant)
            {
                ValueList.Clear();
                if (participant == "Dealer")
                {
                    ValueList.Add(Dealer.DealerFaceDown);
                    foreach (var item in Dealer.DealerFaceUp) 
                    {
                        ValueList.Add(item);
                    }
                }
                else
                {
                    foreach (var item in Player.PlayerFaceUp)
                    {
                        ValueList.Add(item);
                    }
                }
                Total = 0;
                foreach (string item in Valuation.ValueList)
                {
                    switch (item.Substring(item.IndexOf("_") + 1))
                    {
                        case "Ace":
                            AceHigh = true;
                            AceCount += 1;
                            Total += 11;
                            break;
                        case "Jack":
                            Total += 10;
                            break;
                        case "Queen":
                            Total += 10;
                            break;
                        case "King":
                            Total += 10;
                            break;
                        default:
                            Total += Int32.Parse(item.Substring(item.IndexOf("_") + 1));
                            break;
                    }
                }
                while (AceCount > 0 && Total > 21)
                {
                     Total -= 10;
                     AceCount -= 1;
                }
                return Total;
            }
        }
        class Dealer
        {
            public static string DealerFaceDown;
            public static List<string> DealerFaceUp = new List<string>();
        }
        class Player
        {
            public static List<string> PlayerFaceUp = new List<string>();
        }
    }
}