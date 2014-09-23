using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Linq;
using DBDataReaders;
using DB_Connector;
using System.IO;

namespace BI_DB_Connector
{
    class Program
    {
        private static List<string> menuItems = new List<string>();
        private static ArrayList newList = new ArrayList();

        /*
         * Main Method and starting Point
         * no return type
         * */
        static void Main(string[] args)
        {
            string ans = "";
        //backward to Main menu
        mainmenu:
            Console.Clear();
            start();
            ans = MainMenu();
            //main menu Control switch
            switch (ans)
            {
                case "1":
                    Console.Clear();
                    start();
                    CheckConnections();
                    ContinueMessage();
                    goto mainmenu;
                case "2":
                //backward to Select database sub menu
                selectdbtype:
                    Console.Clear();
                    start();
                    menuItems.Clear();
                    menuItems.AddRange(new string[] { "No Sql Database", "Sql Databse", "Main Menu" });
                    ans = DynamicMenu("Select Datbase Type", menuItems);
                    //select database type sub menu control switch
                    switch (ans)
                    {
                        case "1":
                        //backward to NO sql database sub menu
                        nosqldb:
                            Console.Clear();
                            menuItems.Clear();
                            start();
                            menuItems.AddRange(new string[] { "MongoDB", "Go Back", "Main Menu" });
                            ans = DynamicMenu("No Sql Databases", menuItems);
                            //select database type sub menu Control switch
                            switch (ans)
                            {
                                case "1":
                                    //need to create things here
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n\tSorry!!! This is Free Version.Buy Pro Version To Get All features\n");
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    ContinueMessage();
                                    goto nosqldb;
                                case "2":
                                    goto selectdbtype;

                                case "3":
                                    goto mainmenu;
                                default:
                                    WrongInputMessage();
                                    goto nosqldb;
                                //End of select database type sub menu Control switch
                            }
                        case "2":
                        sqldatabase:
                            Console.Clear();
                            menuItems.Clear();
                            start();
                            menuItems.AddRange(new string[] { "PostgerDB", "MysqlDB", "MssqlDB", "Go Back", "Main Menu" });
                            ans = DynamicMenu("Sql Databases", menuItems);
                            //sql database sub menu Control switch
                            switch (ans)
                            {
                                case "1":
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n\tSorry!!! This is Free Version.Buy Pro Version To Get All features\n");
                                    Console.ForegroundColor = ConsoleColor.DarkGreen; ContinueMessage();
                                    goto sqldatabase;
                                case "2":
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n\tSorry!!! This is Free Version.Buy Pro Version To Get All features\n");
                                    Console.ForegroundColor = ConsoleColor.DarkGreen; ContinueMessage();
                                    goto sqldatabase;
                                case "3":
                                options:
                                    Console.Clear();
                                    menuItems.Clear();
                                    start();
                                    menuItems.AddRange(new string[] { "Read Table Data", "Read Relation Ships", "Go Back", "Main Menu" });
                                    ans = DynamicMenu("Options", menuItems);
                                    //sql database sub menu Control switch
                                    switch (ans)
                                    {
                                        case "1":
                                            MssqlReader msDBRead = new MssqlReader();
                                            List<string> tables = msDBRead.GetTables();
                                            PrintTables(tables);
                                            Console.Write("\nEnter Your Request Here :");
                                            string request = Console.ReadLine();
                                            string tName = RequirementGather(request, tables);
                                            if (tName != "")
                                            {
                                                Console.WriteLine("\nData of " + tName + " Table -->\n");
                                                msDBRead.Read(tName);
                                                Console.WriteLine("\n");
                                                ContinueMessage();
                                            }
                                            else
                                            {
                                                WrongInputMessage();
                                            }
                                            goto options;
                                        case "2":
                                            MssqlReader msDBRead1 = new MssqlReader();
                                            List<string> tables1 = msDBRead1.GetTables();
                                            PrintTables(tables1);
                                            //Enter
                                            Console.Write("\nEnter Your Request Here :");
                                            string request1 = Console.ReadLine();
                                            string tName1 = RequirementGather(request1, tables1);
                                            if (tName1 != "")
                                            {
                                                List<string> contentList = GetRelations(tables1, tName1);
                                                Console.WriteLine("\nRalation Tables of " + tName1 + " -->\n");
                                                if (contentList.Count == 1)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("\t" + tName1 + " Not related to any other Table !!!!\n");
                                                    Console.ForegroundColor = ConsoleColor.Green;
                                                }
                                                else
                                                {
                                                    foreach (string table in contentList)
                                                    {
                                                        Console.Write("\t" + table + "\n");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                WrongInputMessage();
                                                goto options;
                                            }
                                            ContinueMessage();
                                            goto options;
                                        case "3":
                                            goto sqldatabase;
                                        case "4":
                                            goto mainmenu;
                                        default:
                                            WrongInputMessage();
                                            goto options;
                                    }
                                case "4":
                                    goto selectdbtype;
                                case "5":
                                    goto mainmenu;
                                default:
                                    WrongInputMessage();
                                    goto sqldatabase;
                            }
                        case "3":
                            goto mainmenu;
                        default:
                            WrongInputMessage();
                            goto selectdbtype;
                    }
                case "3":
                    Console.Clear();
                    start();
                    GetDBInformations();
                    ContinueMessage();
                    goto mainmenu;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    WrongInputMessage();
                    goto mainmenu;
            }
        }

        /*
         * to print starting details not change
         * no return type
         * */
        private static void start()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
            Console.WriteLine(":::                                                                          :::");
            Console.WriteLine("...::::::::::::::::: // Database Relations Finder \\\\ ::::::::::::::::::::::::...");
            Console.WriteLine(":::                                                                          :::");
            Console.Write(":::                                      "); Console.ForegroundColor = ConsoleColor.Cyan; Console.Write("  **** Product By 99XTechnology ****"); Console.ForegroundColor = ConsoleColor.Magenta; Console.Write(":::\n");
            Console.WriteLine("::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n");
        }

        /*
         * to print Main Menu and get the select option
         * return type string
         * */
        private static string MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("     .....................................................................\n");
            Console.WriteLine("     :   1- Check Available Database Connections.                        :\n");
            Console.WriteLine("     :   2- Select a Databases.                                          :\n");
            Console.WriteLine("     :   3- Show Database information.                                   :\n");
            Console.WriteLine("     :   4- Exit.                                                        :\n");
            Console.WriteLine("     :...................................................................:\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string ans = Console.ReadLine();
            return ans;
        }

        /*
         * to print and create dynamic menu 
         * return type string
         * */
        private static string DynamicMenu(string mName, List<string> menuItems)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("........" + mName + "........\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < menuItems.Count(); i++)
            {
                Console.WriteLine("\t" + (i + 1) + " -" + menuItems[i]);
            }
            Console.WriteLine("\n\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            string ans = Console.ReadLine();
            return ans;
        }

        /*
         * for check the connections
         * no return type
         * */
        static private void CheckConnections()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n-----------------Check Connections--------------------\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            //check Mongo Db
            MongoDBConnector mongoCon = new MongoDBConnector();
            Console.WriteLine("....MongoDB....\n");
            StatusWriter(mongoCon.CheckDB());
            Console.WriteLine("\n------------------------------------------------------\n");

            //check Postger Db
            PostgrSqlDbConnector postgerCon = new PostgrSqlDbConnector();
            Console.WriteLine("....PostgerDB....\n");
            StatusWriter(postgerCon.CheckDB());
            Console.WriteLine("\n------------------------------------------------------\n");

            //check Mysql Db
            MySqlDBConnector mysqlCon = new MySqlDBConnector();
            Console.WriteLine("....MysqlDB....\n");
            StatusWriter(postgerCon.CheckDB());
            Console.WriteLine("\n------------------------------------------------------\n");

            //check Mssql Db
            MsSqlDBConnector mssqlCon = new MsSqlDBConnector();
            Console.WriteLine("....Mssql....\n");
            StatusWriter(postgerCon.CheckDB());
            Console.WriteLine("\n------------------------------------------------------\n");
        }

        /*
         * to print hashtable,return from database connectors
         * no return type
         * */
        private static void StatusWriter(Hashtable status)
        {
            foreach (DictionaryEntry child in status)
            {
                Console.WriteLine(child.Key + " " + child.Value + "\n");
            }
        }

        /*
         * to indicate when wron input inserted
         * no return type
         * */
        private static void WrongInputMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n......Your request is wrong please try again!!......\n");
            Console.ForegroundColor = ConsoleColor.Green;
            ContinueMessage();
        }

        /*
         * to hold the screen when user want to goback
         * no return type
         * */
        private static void ContinueMessage()
        {
            Console.WriteLine("\nPress ENTER key to continue");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        again:
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key != ConsoleKey.Enter)
            {
                goto again;
            }
        }

        /*
         * to check element availability of list
         * return type bool
         * */
        private static bool CheckElementAvailabilityOfList(List<string> list, string element)
        {
            if (list.Contains(element))
                return true;
            else
                return false;
        }

        /*
        * to Print tables of selected database
        * no return type
        * */
        private static void PrintTables(List<string> list)
        {
            Console.WriteLine("Tables of Selected Database -->\n");
            foreach (string table in list)
            {
                Console.Write("\t" + table + "\n");
            }
        }

        /*
       * to Call MappingRelations method to merege relationships and to find match relational tables with request table
       * return type string List
       * */
        private static List<string> GetRelations(List<string> list, string table)
        {
            List<string> contentList = new List<string>();
            ArrayList aList = MappingRelations(list);
            foreach (List<string> rList in aList)
            {
                if (rList.Contains(table))
                {
                    contentList = new List<string>(rList);
                }
            }
            return contentList;
        }

        /*
      * to Merege single relations with Particular relations 
      * return type string Array List
      * */
        private static ArrayList MappingRelations(List<string> list)
        {
            MssqlReader newReader = new MssqlReader();
            foreach (string table in list)
            {
                newList.Add(newReader.ReadRelations(table));
            }
            RelationsMapper newMapper = new RelationsMapper();
            newList = newMapper.MergeJoins(newList);
            return newList;
        }
        private static string RequirementGather(string request, List<string> tables)
        {
            string requireTableName = "";
            foreach (string tableName in tables)
            {
                if (request.Contains(tableName) && request.Length != tableName.Length)
                {
                    requireTableName = tableName;
                }
            }
            return requireTableName;
        }
        private static void GetDBInformations()
        {
            Console.WriteLine("Configered Database informations -->\n");
            Console.WriteLine("\nDatabase Name :{0} \nServer Name :{1}\n ", System.Configuration.ConfigurationManager.AppSettings["mongodatabase"], System.Configuration.ConfigurationManager.AppSettings["mongodbconString"]);
            Console.WriteLine("\nDatabase Name :{0} \nServer Name :{1}\n ", System.Configuration.ConfigurationManager.AppSettings["mysqlDatabase"], System.Configuration.ConfigurationManager.AppSettings["mysqlServer"]);
            Console.WriteLine("\nDatabase Name :{0} \nServer Name :{1}\n ", System.Configuration.ConfigurationManager.AppSettings["initialCatalog"], System.Configuration.ConfigurationManager.AppSettings["dataSource"]);
            Console.WriteLine("\nDatabase Name :{0} \nServer Name :{1}\n ", System.Configuration.ConfigurationManager.AppSettings["postgerDatabase"], System.Configuration.ConfigurationManager.AppSettings["postgerHost"]);
        }
    }
}
