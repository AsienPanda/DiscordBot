using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    class Commands
    {
        public static void TestCommand(DiscordClient bot, CommandService commands)
        {
            //Example of individual command
            commands.CreateCommand("greet") //create command greet
            .Alias(new string[] { "gr", "hi" }) //add 2 aliases, so it can be run with ~gr and ~hi
            .Description("Greets a person.") //add description, it will be shown when ~help is used
            .Parameter("GreetedPerson", ParameterType.Required) //as an argument, we have a person we want to greet
            .Do(async e =>
            {
                await e.Channel.SendMessage(e.User.Name + " greets " + e.GetArg("GreetedPerson"));
                //sends a message to channel with the given text
            });

            commands.CreateCommand("roll")
            .Alias(new string[] { "dice" })
            .Description("Rolls dice.")
            .Parameter("NumberOfDice", ParameterType.Required)
            .Parameter("NumberOfSides", ParameterType.Required)
            .Parameter("AddFlag", ParameterType.Optional)
            .Do(async e =>
             {
                 int numDice = Int32.Parse(e.GetArg("NumberOfDice"));
                 int numSides = Int32.Parse(e.GetArg("NumberOfSides"));

                 if (numDice > 100)
                 {
                     await e.Channel.SendMessage("_You cannot roll more than 100 dice!_");
                 }
                 else if (numSides > 999)
                 {
                     await e.Channel.SendMessage("_Dice cannot have more than 999 sides!_");
                 }
                 else {
                     int sum = 0;
                     int[] array = new int[numDice];

                     Random rnd = new Random();
                     for (int i = 0; i < numDice; ++i)
                     {
                         array[i] = rnd.Next(0, numSides) + 1;
                         sum += array[i];

                     }
                     String msg = String.Format("{0}{1}", "```", array[0]);
                     for (int i = 1; i < numDice; ++i)
                     {
                         msg = String.Format("{0}, {1}", msg, array[i]);
                     }
                     msg = msg + "```";
                     await e.Channel.SendMessage(msg);
                     if (Boolean.Parse(e.GetArg("AddFlag")))
                     {
                         await e.Channel.SendMessage(String.Format("_The sum is: {0}_", sum));
                     }
                 }
             });
            commands.CreateCommand("choose")
                .Alias(new string[] { "erabe" })
                .Description("Randomly decides a choice. Multi-word answers must be used in \"quotation marks\"!")
                .Parameter("option", ParameterType.Required)
                .Parameter("option", ParameterType.Required)
                .Parameter("option", ParameterType.Multiple)
                .Do(async e =>
                {
                    //redefine parameters
                    List<String> list = new List<String>();
                    for (int i = 0; i < e.Args.Length; ++i)
                    {

                        if (e.GetArg(i).Contains("\""))
                        {
                            ++i;
                            String s = "";
                            while (e.GetArg(i) != "\"" && i < e.Args.Length - 1)
                            {
                                s = s + e.GetArg(i) + " ";
                                ++i;
                            }

                            if (e.GetArg(i).Contains("\""))
                            {
                                list.Add(s);
                            }
                        }
                        else {
                            list.Add(e.GetArg(i));
                        }

                    }
                    if (list.Count < 2)
                    {
                        await e.Channel.SendMessage("_You must enter at least 2 options!_");
                    }
                    else {
                        Random rnd = new Random();

                        await e.Channel.SendMessage(String.Format("_Misaka chooses **{0}**_", list.ElementAt(rnd.Next(0, list.Count))));
                    }


                });

            //Example of command group
            //we would run our commands with ~do greet X and ~do bye X
            commands.CreateGroup("do", cgb =>
            {
                cgb.CreateCommand("greet")
                        .Alias(new string[] { "gr", "hi" })
                        .Description("Greets a person.")
                        .Parameter("GreetedPerson", ParameterType.Required)
                        .Do(async e =>
                        {
                            await e.Channel.SendMessage(e.User.Name + " greets " + e.GetArg("GreetedPerson"));
                        });

                cgb.CreateCommand("bye")
                        .Alias(new string[] { "bb", "gb" })
                        .Description("Greets a person.")
                        .Parameter("GreetedPerson", ParameterType.Required)
                        .Do(async e =>
                        {
                            await e.Channel.SendMessage(e.User.Name + " says goodbye to " + e.GetArg("GreetedPerson"));
                        });
            });
        }
    }
}
