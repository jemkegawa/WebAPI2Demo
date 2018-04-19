using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio.AspNet.Common;
using WebAPI2Demo.TheData;

namespace WebAPI2Demo.TheBusiness
{
    public static class SMSResponseMessages
    {
        public static string Woof(string phoneNumber)
        {
            return "Bork!";
        }

        public static string MissingValueMessage(string phoneNumber, string valueNeeded, string example = "")
        {
            var exampleString = "";
            if (example != string.Empty)
                exampleString = "\nExample Command: " + example;

            return "bweep boop w00f! You forgot to send me your " + valueNeeded + "." + exampleString;
        }

        public static string UnknownPhoneMessage(string phoneNumber)
        {
            return "Hello friendly human! Do not be alarmed, I am just a friendly bot, but I don't recognize you. To signup and enjoy the wonders of this service, send the word \"join\" and your name!";
        }

        public static string UnknownCommandMessage(string phoneNumber)
        {
            return "Hello again friendly human! I'm sorry, but I didn't understand that. Try sending back one of the following commands to use me:\n\"joke\"\n\"cat\"\n...";
        }

        public static string ErrorMessage(string phoneNumber)
        {
            return "beep boop aW000ooooooo!!! (Sorry, looks like I've had an error)";
        }
        
        public static string ExceptionErrorMessage(string phoneNumber)
        {
            return "beep boop aW000ooooooo!!! (Sorry, looks like I've had a critical error)";
        }

        public static string NotImplementedMessage(string phoneNumber)
        {
            return "Hello again! Unfortunately, this functionality is not implemented yet. ";
        }

        public static string AlreadyRegisteredMessage(string phoneNumber)
        {
            return "You are already registered! Try updating your email with the word \"email\" + your email address. Or text the word \"joke\" to hear a joke :)";
        }

        public static string JustRegisteredMessage(string phoneNumber)
        {
            return "Welcome! Reply \"email\" and your email address next to signup for sweet updates! Reply \"Unregister\" at any time to unregister and be removed from this service.";
        }

        public static string CommandsMessage(string phoneNumber)
        {
            return "Thank you, you're all set! Send back one of the following commands to use me: \"joke\", \"cat\", ...";
        }

        public static string UnregisteredMessage(string phoneNumber)
        {
            return "I'm sad to see you go, but you'll receive no more messages from me unless you join or message me again. Thank you!";
        }

        public static string Joke(string phoneNumber)
        {
            var jokes = new List<string>
            {
                "What time did the man go to the dentist?\nTooth hurt-y.",
                "Did you hear about the guy who invented Lifesavers?\nThey say he made a mint.",
                "A ham sandwich walks into a bar and orders a beer. Bartender says, 'Sorry we don't serve food here.'",
                "Whenever the cashier at the grocery store asks my dad if he would like the milk in a bag he replies, 'No, just leave it in the carton!'",
                "Why do chicken coops only have two doors?\nBecause if they had four, they would be chicken sedans!",
                "Why did the Clydesdale give the pony a glass of water?\nBecause he was a little horse!",
                "Me: 'Hey, I was thinking…'\nMy dad: 'I thought I smelled something burning.'",
                "How do you make holy water?\nYou boil the hell out of it.",
                "5/4 of people admit that they’re bad with fractions."
            };

            var random = new Random();
            var joke = jokes[random.Next(1, jokes.Count)];

            return joke;
        }
    }
}