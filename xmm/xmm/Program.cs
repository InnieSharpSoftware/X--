#region Libraries
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;
#endregion

namespace xmm
{
	class Program
	{
		#region Variables
		public const string NAME = "X--";
		public const string VER = "DEVELOP DD/MM/YYYY 09/12/2022";
		public const string DEVS = "TeBrado~Innie#, #Moon~RedMoon";
		public const string FULLINFO = NAME + " " + VER + " By ISSU: " + DEVS;
		public static void ShowFullInfo() { Console.WriteLine(FULLINFO); } // not variable but...
		
		public static Dictionary<byte, string> cms = new Dictionary<byte, string>
		{
			{0, "exit"},
			{1, "showchar"},
			{2, "getchar"},
			{3, "getfile"},
			{4, "showfile"},
			{5, "showinfo"},
			{6, "pause"},
		};
		
		#region SHOWCHAR
		
		public static char? scsymbol;
		public static ConsoleColor? scforeColor;
		public static ConsoleColor? scbackColor;
		
		public static void scInit()
		{
			if(scsymbol != null)
			{
				ConsoleColor? sclastForeColor = (ConsoleColor?)Console.ForegroundColor;
				ConsoleColor? sclastBackColor = (ConsoleColor?)Console.BackgroundColor;
				if(scforeColor != null) { Console.ForegroundColor = (ConsoleColor)scforeColor; }
				if(scbackColor != null) { Console.BackgroundColor = (ConsoleColor)scbackColor; }
				Console.Write(scsymbol);
				Console.ForegroundColor = (ConsoleColor)sclastForeColor;
				Console.BackgroundColor = (ConsoleColor)sclastBackColor;
				sclastForeColor = null; sclastBackColor = null; scforeColor = null; scsymbol = null;
			}
		}
		
		#endregion
		
		#region PAUSE
		
		public static void pInit() { Console.ReadKey(true); }
		
		#endregion
		
		#endregion
		
		#region BASE
		
		public static void Main(string[] args)
		{
			string path = "gjurhuiihijhghrhgjhfjdkhgkjfhdjhdghuiehr9h1h4vh.//"; string[] l;
			foreach(string s in args)
				path = s;
			if(!File.Exists(path))
				if(File.Exists("main.xmm"))
					path = "main.xmm";
			if(File.Exists(path))
			{
				l = File.ReadAllLines(path);
				for(int i = 0; i < l.Length; i++)
				{
					try
					{
						if (l[i].StartsWith("push "))
						{
							string[] def = l[i].Remove(0, 5).Split(new string[] { " " }, StringSplitOptions.None);
							byte command = 0;
							try { command = byte.Parse(def[0]); }
							catch {}
							int section = 0;
							try { section = int.Parse(def[1]); }
							catch {}
							string value = def[2];
							if(cms.ContainsKey(command))
							{
								if(cms[command] == "showchar")
								{
									if(section == 0)
									{
										if(value == "/n")
											scsymbol = '\n';
										else if(value == "/s")
											scsymbol = '\u0020';
										else if(value == "/t")
											scsymbol = '\t';
										else if(value == "/0")
											scsymbol = '\0';
										else
										{
											try { scsymbol = char.Parse(value); }
											catch { scsymbol = '0'; }
										}
									}
									else if(section == 1)
									{
										try { scforeColor = (ConsoleColor)int.Parse(value); }
										catch { scforeColor = (ConsoleColor)0;}
									}
									else if(section == 2)
									{
										try { scbackColor = (ConsoleColor)int.Parse(value); }
										catch { scbackColor = (ConsoleColor)0;}
									}
								}
							}
						}
						else if (l[i].StartsWith("init "))
						{
							string def = l[i].Remove(0, 5);
							switch (def)
							{
								default:
									break;
								
								case "1":
									scInit();
									break;
								case "6":
									pInit();
									break;
							}
						}
					}
					catch(Exception ex)
					{
						File.WriteAllText("log-" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + "_errors.txt", "Line(" + i + "): " + ex.Message);
					}
				}
			}
		}
		
		#endregion
	}
}