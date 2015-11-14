using System;
using System.Collections.Generic;
using System.Threading;
using ATSProject.Enums;
using ATSProject.Interfaces;
using ATSProject.Model;
using ATSProject.Model.ATS;

namespace ATSProject.TestClasses
{
    public class TestStation : Station
    {
        public TestStation(IList<IPort> ports, IList<ITerminal> terminals)
            : base(ports, terminals)
        {
        }

        public override Call CallingImulation(CallInfo info)
        {
            ITerminal targetTerminal = TerminalByPhoneNumber(info.Target);
            int rand = new Random().Next(2);
            TimeSpan duration = new TimeSpan();
            if (rand == 1)
            {
                targetTerminal.AnswerTheCall(info);
                info.Result = Result.Success;
                int minutes = new Random().Next(60), seconds = new Random().Next(60);
                Thread.Sleep(minutes*100);
                duration = new TimeSpan(0, minutes, seconds);
            }
            else
            {
                targetTerminal.DropTheCall(info);
                info.Result = Result.Fail;
            }
            return new Call(info, new CallStatistic { Duration = duration });
        }

        public override void SubscriptionToTerminalEvents(ITerminal terminal)
        {
            terminal.OutgoingRequest += (sender, info) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(info.ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
            };
            terminal.IncomingRequest += (sender, info) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(info.ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
            };
            terminal.StateChanged += (sender, state) => { Console.WriteLine(sender.ToString()); };
            terminal.AnsweredTheCall += (sender, result) =>
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                var term = sender as Terminal;
                if (term != null)
                    Console.WriteLine(term.PhoneNumber.ToString() + " answer the call");
                Console.ForegroundColor = ConsoleColor.Gray;
            };
            terminal.DroppedTheCall += (sender, result) =>
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                var term = sender as Terminal;
                if (term != null)
                    Console.WriteLine(term.PhoneNumber.ToString() + " drop the call");
                Console.ForegroundColor = ConsoleColor.Gray;
            };
        }

        public override void SubscriptionToPortEvents(IPort port)
        {
            port.StateChanged += (sender, state) => { Console.WriteLine(sender.ToString()); };
        }
    }
}
