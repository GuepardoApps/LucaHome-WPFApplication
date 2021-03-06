﻿using System.Collections.Generic;

namespace Common.Enums
{
    public class LucaServerAction
    {
        public static readonly LucaServerAction NULL = new LucaServerAction(0, "");

        //BIRTHDAYS
        public static readonly LucaServerAction GET_BIRTHDAYS = new LucaServerAction(10, "getbirthdays");
        public static readonly LucaServerAction ADD_BIRTHDAY = new LucaServerAction(11, "addbirthday&id=");
        public static readonly LucaServerAction UPDATE_BIRTHDAY = new LucaServerAction(12, "updatebirthday&id=");
        public static readonly LucaServerAction DELETE_BIRTHDAY = new LucaServerAction(13, "deletebirthday&id=");

        //CAMERA
        public static readonly LucaServerAction START_MOTION = new LucaServerAction(20, "startmotion");
        public static readonly LucaServerAction STOP_MOTION = new LucaServerAction(21, "stopmotion");
        public static readonly LucaServerAction GET_MOTION_DATA = new LucaServerAction(22, "getmotiondata");
        public static readonly LucaServerAction SET_MOTION_CONTROL_TASK = new LucaServerAction(23, "setcontroltaskcamera&state=");

        //CHANGE
        public static readonly LucaServerAction GET_CHANGES = new LucaServerAction(30, "getchangesrest");

        //COINS
        public static readonly LucaServerAction GET_COINS_ALL = new LucaServerAction(130, "getcoinsall");
        public static readonly LucaServerAction GET_COINS_USER = new LucaServerAction(131, "getcoinsuser");
        public static readonly LucaServerAction ADD_COIN = new LucaServerAction(132, "addcoin&id=");
        public static readonly LucaServerAction UPDATE_COIN = new LucaServerAction(133, "updatecoin&id=");
        public static readonly LucaServerAction DELETE_COIN = new LucaServerAction(134, "deletecoin&id=");

        //INFORMATION
        public static readonly LucaServerAction GET_INFORMATIONS = new LucaServerAction(40, "getinformationsrest");

        //LISTEDMENU
        public static readonly LucaServerAction GET_LISTEDMENU = new LucaServerAction(50, "getlistedmenu");
        public static readonly LucaServerAction ADD_LISTEDMENU = new LucaServerAction(51, "addlistedmenu&id=");
        public static readonly LucaServerAction UPDATE_LISTEDMENU = new LucaServerAction(52, "updatelistedmenu&id=");
        public static readonly LucaServerAction DELETE_LISTEDMENU = new LucaServerAction(53, "deletelistedmenu&id=");

        //MENU
        public static readonly LucaServerAction GET_MENU = new LucaServerAction(54, "getmenu");
        public static readonly LucaServerAction UPDATE_MENU = new LucaServerAction(55, "updatemenu&weekday=");
        public static readonly LucaServerAction CLEAR_MENU = new LucaServerAction(56, "clearmenu&weekday=");

        //MAP_CONTENT
        public static readonly LucaServerAction GET_MAP_CONTENTS = new LucaServerAction(60, "getmapcontents");
        public static readonly LucaServerAction ADD_MAP_CONTENT = new LucaServerAction(61, "addmapcontent&id=");
        public static readonly LucaServerAction UPDATE_MAP_CONTENT = new LucaServerAction(62, "updatemapcontent&id=");
        public static readonly LucaServerAction DELETE_MAP_CONTENT = new LucaServerAction(63, "deletemapcontent&id=");

        //MOVIE
        public static readonly LucaServerAction GET_MOVIES = new LucaServerAction(70, "getmovies");
        public static readonly LucaServerAction START_MOVIE = new LucaServerAction(71, "startmovie&id=");
        public static readonly LucaServerAction UPDATE_MOVIE = new LucaServerAction(72, "updatemovie&id=");

        //SCHEDULES
        public static readonly LucaServerAction GET_SCHEDULES = new LucaServerAction(80, "getschedules");
        public static readonly LucaServerAction SET_SCHEDULE = new LucaServerAction(81, "setschedule&id=");
        public static readonly LucaServerAction ADD_SCHEDULE = new LucaServerAction(82, "addschedule&id=");
        public static readonly LucaServerAction UPDATE_SCHEDULE = new LucaServerAction(83, "updateschedule&id=");
        public static readonly LucaServerAction DELETE_SCHEDULE = new LucaServerAction(84, "deleteschedule&id=");

        //SHOPPING_LIST
        public static readonly LucaServerAction GET_SHOPPING_LIST = new LucaServerAction(90, "getshoppinglist");
        public static readonly LucaServerAction ADD_SHOPPING_ENTRY_F = new LucaServerAction(91, "addshoppingentry&id={0}&name={1}&group={2}&quantity={3}&unit={4}");
        public static readonly LucaServerAction UPDATE_SHOPPING_ENTRY_F = new LucaServerAction(92, "updateshoppingentry&id={0}&name={1}&group={2}&quantity={3}&unit={4}");
        public static readonly LucaServerAction DELETE_SHOPPING_ENTRY_F = new LucaServerAction(93, "deleteshoppingentry&id={0}");

        //SOCKETS
        public static readonly LucaServerAction GET_SOCKETS = new LucaServerAction(100, "getsockets");
        public static readonly LucaServerAction SET_SOCKET = new LucaServerAction(101, "setsocket&socket=");
        public static readonly LucaServerAction ADD_SOCKET = new LucaServerAction(102, "addsocket&typeid=");
        public static readonly LucaServerAction UPDATE_SOCKET = new LucaServerAction(103, "updatesocket&typeid=");
        public static readonly LucaServerAction DELETE_SOCKET = new LucaServerAction(104, "deletesocket&typeid=");
        public static readonly LucaServerAction DEACTIVATE_ALL_SOCKETS = new LucaServerAction(105, "deactivateAllSockets");

        //SWITCH
        public static readonly LucaServerAction GET_SWITCHES = new LucaServerAction(110, "getswitches");
        public static readonly LucaServerAction ADD_SWITCH = new LucaServerAction(111, "addswitch&id=");
        public static readonly LucaServerAction UPDATE_SWITCH = new LucaServerAction(112, "updateswitch&id=");
        public static readonly LucaServerAction DELETE_SWITCH = new LucaServerAction(113, "deleteswitch&id=");
        public static readonly LucaServerAction TOGGLE_SWITCH = new LucaServerAction(114, "toggleswitch&name=");
        public static readonly LucaServerAction TOGGLE_ALL_SWITCHES = new LucaServerAction(115, "toggleallswitches");

        //METER_DATA
        public static readonly LucaServerAction GET_METER_DATA = new LucaServerAction(120, "getmeterdata");
        public static readonly LucaServerAction ADD_METER_DATA = new LucaServerAction(121, "addmeterdata&id=");
        public static readonly LucaServerAction UPDATE_METER_DATA = new LucaServerAction(122, "updatemeterdata&id=");
        public static readonly LucaServerAction DELETE_METER_DATA = new LucaServerAction(123, "deletemeterdata&id=");

        //MONEY METER_DATA
        public static readonly LucaServerAction GET_MONEY_METER_DATA_ALL = new LucaServerAction(130, "getmoneymeterdataall");
        public static readonly LucaServerAction GET_MONEY_METER_DATA_USER = new LucaServerAction(131, "getmoneymeterdatauser");
        public static readonly LucaServerAction ADD_MONEY_METER_DATA = new LucaServerAction(132, "addmoneymeterdata&id=");
        public static readonly LucaServerAction UPDATE_MONEY_METER_DATA = new LucaServerAction(133, "updatemoneymeterdata&id=");
        public static readonly LucaServerAction DELETE_MONEY_METER_DATA = new LucaServerAction(134, "deletemoneymeterdata&id=");

        //TEMPERATURE
        public static readonly LucaServerAction GET_TEMPERATURES = new LucaServerAction(140, "getcurrenttemperature");

        //USER
        public static readonly LucaServerAction VALIDATE_USER = new LucaServerAction(150, "validateuser");

        public static IEnumerable<LucaServerAction> Values
        {
            get
            {
                yield return NULL;

                yield return GET_BIRTHDAYS;
                yield return ADD_BIRTHDAY;
                yield return UPDATE_BIRTHDAY;
                yield return DELETE_BIRTHDAY;

                yield return START_MOTION;
                yield return STOP_MOTION;
                yield return GET_MOTION_DATA;
                yield return SET_MOTION_CONTROL_TASK;

                yield return GET_CHANGES;

                yield return GET_INFORMATIONS;

                yield return GET_MAP_CONTENTS;
                yield return ADD_MAP_CONTENT;
                yield return UPDATE_MAP_CONTENT;
                yield return DELETE_MAP_CONTENT;

                yield return GET_LISTEDMENU;
                yield return ADD_LISTEDMENU;
                yield return UPDATE_LISTEDMENU;
                yield return DELETE_LISTEDMENU;

                yield return GET_MENU;
                yield return UPDATE_MENU;
                yield return CLEAR_MENU;

                yield return GET_MOVIES;
                yield return START_MOVIE;
                yield return UPDATE_MOVIE;

                yield return GET_SCHEDULES;
                yield return SET_SCHEDULE;
                yield return ADD_SCHEDULE;
                yield return UPDATE_SCHEDULE;
                yield return DELETE_SCHEDULE;

                yield return GET_SHOPPING_LIST;
                yield return ADD_SHOPPING_ENTRY_F;
                yield return UPDATE_SHOPPING_ENTRY_F;
                yield return DELETE_SHOPPING_ENTRY_F;

                yield return GET_SOCKETS;
                yield return SET_SOCKET;
                yield return ADD_SOCKET;
                yield return UPDATE_SOCKET;
                yield return DELETE_SOCKET;
                yield return DEACTIVATE_ALL_SOCKETS;

                yield return GET_SWITCHES;
                yield return ADD_SWITCH;
                yield return UPDATE_SWITCH;
                yield return DELETE_SWITCH;
                yield return TOGGLE_SWITCH;
                yield return TOGGLE_ALL_SWITCHES;

                yield return GET_METER_DATA;
                yield return ADD_METER_DATA;
                yield return UPDATE_METER_DATA;
                yield return DELETE_METER_DATA;

                yield return GET_MONEY_METER_DATA_ALL;
                yield return GET_MONEY_METER_DATA_USER;
                yield return ADD_MONEY_METER_DATA;
                yield return UPDATE_MONEY_METER_DATA;
                yield return DELETE_MONEY_METER_DATA;

                yield return GET_TEMPERATURES;

                yield return VALIDATE_USER;
            }
        }

        private readonly int _id;
        private readonly string _action;

        LucaServerAction(int id, string action)
        {
            _id = id;
            _action = action;
        }

        public int Id
        {
            get
            {
                return _id;
            }
        }

        public string Action
        {
            get
            {
                return _action;
            }
        }

        public override string ToString()
        {
            return _action;
        }
    }
}
