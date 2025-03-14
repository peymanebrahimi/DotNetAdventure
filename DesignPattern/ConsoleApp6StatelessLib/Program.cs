﻿// See https://aka.ms/new-console-template for more information

using ConsoleApp6StatelessLib;

Console.WriteLine("Hello, World!");

var phoneCall = new PhoneCall("Lokesh");            

phoneCall.Print();
phoneCall.Dialed("Prameela");
phoneCall.Print();
phoneCall.Connected();
phoneCall.Print();
phoneCall.SetVolume(2);
phoneCall.Print();
phoneCall.Hold();
phoneCall.Print();
phoneCall.Mute();
phoneCall.Print();
phoneCall.Unmute();
phoneCall.Print();
phoneCall.Resume();
phoneCall.Print();
phoneCall.SetVolume(11);
phoneCall.Print();


Console.WriteLine(phoneCall.ToDotGraph());

Console.WriteLine("Press any key...");
Console.ReadKey(true);