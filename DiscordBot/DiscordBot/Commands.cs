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
