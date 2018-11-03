﻿using System;
using System.Collections.Generic;

namespace HomeAutomationProtocoll
{
    public static class GetData
    {
        public static string ValueFromDeviceDictionary( Dictionary<uint, string> dic, uint key )
        {
            // Try to get the result in the static Dictionary
            string result;
            if (dic.TryGetValue( key, out result ))
            {
                return result;
            }
            else
            {
                return "";
            }
        }
    }
    public static class ComandoString
    {
        public const string NONE = "NONE";
        public const string ON = "ON";
        public const string OFF = "OFF";
        public const string EXIT = "EXIT";
        public const string BLINK = "BLINK";
        public const string TURN_ALL_LIGHTS_OFF = "TURN-ALL-LIGHTS-OFF";
        public const string TURN_ALL_LIGHTS_ON = "TURN-ALL-LIGHTS-ON";
        public const string TURN_ALL_KITCHEN_LIGHTS_OFF = "TURN-ALL-KITCHEN-LIGHTS-OFF";
        public const string TURN_ALL_KITCHEN_LIGHTS_ON = "TURN-ALL-KITCHEN-LIGHTS-ON";
        public const string TURN_KITCHEN_LIGHTS_CABINET_ON = "TURN-KITCHEN-LIGHTS-CABINET-ON";           // Lichter über Küschenschrank
        public const string TURN_KITCHEN_LIGHTS_CABINET_OFF = "TURN-KITCHEN-LIGHTS-CABINET-OFF";
        public const string TURN_KITCHEN_LIGHT_FUMEHOOD_ON = "TURN-KITCHEN-LIGHT-FUMEHOOD-ON";           // Lichbalken an der Dunstabzughaube
        public const string TURN_KITCHEN_LIGHT_FUMEHOOD_OFF = "TURN-KITCHEN-LIGHT-FUMEHOOD-OFF";
        public const string TURN_KITCHEN_LIGHT_SLOT_ON = "TURN-KITCHEN-LIGHT-SLOT-ON";               // Lichter in der Ecke neben den Dustabzug
        public const string TURN_KITCHEN_LIGHT_SLOT_OFF = "TURN-KITCHEN-LIGHT-SLOT-OFF";
        public const string TURN_KITCHEN_LIGHT_OVER_CABINET_RIGHT_ON = "TURN-KITCHEN-LIGHT-OVER-CABINET-RIGHT-ON"; // Lichtbalken über Schrank rechts ( E - Verteiler )
        public const string TURN_KITCHEN_LIGHT_OVER_CABINET_RIGHT_OFF = "TURN-KITCHEN-LIGHT-OVER-CABINET-RIGHT-OFF";
        public const string TURN_KITCHEN_FRONT_LIGHT_1_ON = "TURN-KITCHEN-FRONT-LIGHT-1-ON";
        public const string TURN_KITCHEN_FRONT_LIGHT_2_ON = "TURN-KITCHEN-FRONT-LIGHT-2-ON";
        public const string TURN_KITCHEN_FRONT_LIGHT_3_ON = "TURN-KITCHEN-FRONT-LIGHT-3-ON";
        public const string TURN_KITCHEN_FRONT_LIGHT_1_OFF = "TURN-KITCHEN-FRONT-LIGHT-1-OFF";
        public const string TURN_KITCHEN_FRONT_LIGHT_2_OFF = "TURN-KITCHEN-FRONT-LIGHT-2-OFF";
        public const string TURN_KITCHEN_FRONT_LIGHT_3_OFF = "TURN-KITCHEN-FRONT-LIGHT-3-OFF";

        public const string TURN_GALLERY_UP_ON = "TURN-GALLERY-UP-ON";
        public const string TURN_GALLERY_UP_OFF = "TURN-GALLERY-UP-OFF";
        public const string TURN_GALLERY_UP1_ON = "TURN-GALLERY-UP1-ON";
        public const string TURN_GALLERY_UP2_ON = "TURN-GALLERY-UP2-ON";
        public const string TURN_GALLERY_UP3_ON = "TURN-GALLERY-UP3-ON";
        public const string TURN_GALLERY_UP4_ON = "TURN-GALLERY-UP4-ON";
        public const string TURN_GALLERY_UP5_ON = "TURN-GALLERY-UP5-ON";
        public const string TURN_GALLERY_UP6_ON = "TURN-GALLERY-UP6-ON";
        public const string TURN_GALLERY_UP7_ON = "TURN-GALLERY-UP7-ON";
        public const string TURN_GALLERY_UP8_ON = "TURN-GALLERY-UP8-ON";
        public const string TURN_GALLERY_UP9_ON = "TURN-GALLERY-UP9-ON";
        public const string TURN_GALLERY_UP10_ON = "TURN-GALLERY-UP10-ON";
        public const string TURN_GALLERY_UP11_ON = "TURN-GALLERY-UP11-ON";
        public const string TURN_GALLERY_UP12_ON = "TURN-GALLERY-UP12-ON";

        public const string TURN_GALLERY_UP1_OFF = "TURN-GALLERY-UP1-OFF";
        public const string TURN_GALLERY_UP2_OFF = "TURN-GALLERY-UP2-OFF";
        public const string TURN_GALLERY_UP3_OFF = "TURN-GALLERY-UP3-OFF";
        public const string TURN_GALLERY_UP4_OFF = "TURN-GALLERY-UP4-OFF";
        public const string TURN_GALLERY_UP5_OFF = "TURN-GALLERY-UP5-OFF";
        public const string TURN_GALLERY_UP6_OFF = "TURN-GALLERY-UP6-OFF";
        public const string TURN_GALLERY_UP7_OFF = "TURN-GALLERY-UP7-OFF";
        public const string TURN_GALLERY_UP8_OFF = "TURN-GALLERY-UP8-OFF";
        public const string TURN_GALLERY_UP9_OFF = "TURN-GALLERY-UP9-OFF";
        public const string TURN_GALLERY_UP10_OFF = "TURN-GALLERY-UP10-OFF";
        public const string TURN_GALLERY_UP11_OFF = "TURN-GALLERY-UP11-OFF";
        public const string TURN_GALLERY_UP12_OFF = "TURN-GALLERY-UP12-OFF";

        public const string TURN_TRIANGLE_UPSTAIRS_ON = "TURN-TRIANAGLE-UPSTAIRS-ON";                       // ganz oben 2 Leuchten beim "Dreieck"
        public const string TURN_TRIANGLE_UPSTAIRS_OFF = "TURN-TRIANAGLE-UPSTAIRS-OFF";
        public const string TURN_LIGHTBAR_OVER_DOOR_ENTRY_ON = "TURN-LIGHTBAR-OVER-DOOR-ENTRY-ON";
        public const string TURN_LIGHTBAR_OVER_DOOR_ENTRY_OFF = "TURN-LIGHTBAR-OVER-DOOR-ENTRY-OFF";
        public const string TURN_LIGHTBAR_OVER_RIGHTWINDOW_BESIDE_DOOR_ON = "TURN-LIGHTBAR-OVER-RIGHTWINDOW-BESIDE-DOOR-ON";
        public const string TURN_LIGHTBAR_OVER_RIGHTWINDOW_BESIDE_DOOR_OFF = "TURN-LIGHTBAR-OVER-RIGHTWINDOW-BESIDE-DOOR-OFF";
        public const string TURN_LIGHT_WINDOW_SOUTHEAST_UPSIDE_ON = "TURN-LIGHT-WINDOW-SOUTHEAST-UPSIDE-ON";
        public const string TURN_LIGHT_WINDOW_SOUTHEAST_UPSIDE_OFF = "TURN-LIGHT-WINDOW-SOUTHEAST-UPSIDE-OFF";

        public const string TURN_GALLERY_DOWN_ON = "TURN-GALLERY-DOWN-ON";
        public const string TURN_GALLERY_DOWN_OFF = "TURN-GALLERY-DOWN-OFF";
        public const string TURN_GALLERY_DOWN_1_ON = "TURN-GALLERY-DOWN-1-ON";                          // Reihe 1 vorne
        public const string TURN_GALLERY_DOWN_2_ON = "TURN-GALLERY-DOWN-2-ON";                          // Reihe 2 vorne
        public const string TURN_GALLERY_DOWN_3_ON = "TURN-GALLERY-DOWN-3-ON";                          // Reihe 3 hinten
        public const string TURN_GALLERY_DOWN_4_ON = "TURN-GALLERY-DOWN-4-ON";                          // Reihe 4 hinten
        public const string TURN_GALLERY_DOWN_1_OFF = "TURN-GALLERY-DOWN-1-OFF";
        public const string TURN_GALLERY_DOWN_2_OFF = "TURN-GALLERY-DOWN-2-OFF";
        public const string TURN_GALLERY_DOWN_3_OFF = "TURN-GALLERY-DOWN-3-OFF";
        public const string TURN_GALLERY_DOWN_4_OFF = "TURN-GALLERY-DOWN-4-OFF";

        public const string TURN_BOILER_ON = "TURN-BOILER-ON";
        public const string TURN_BOILER_OFF = "TURN-BOILER-OFF";
        public const string TURN_FRONT_LIGHTS_ON = "TURN-FRONT-LIGHTS-ON";
        public const string TURN_FRONT_LIGHTS_OFF = "TURN-FRONT-LIGHTS-OFF";                           // Vorderlichter Küche über der Arbeitsfläche
        public const string TURN_WINDOW_LEDGE_EAST_ON = "TURN-WINDOW-LEDGE-EAST-ON";
        public const string TURN_WINDOW_LEDGE_EAST_OFF = "TURN-WINDOW-LEDGE-EAST-OFF";                      // Fensterbank ost seitig
        public const string TURN_WINDOW_LEDGE_WEST_ON = "TURN-WINDOW-LEDGE-WEST-ON";
        public const string TURN_WINDOW_LEDGE_WEST_OFF = "TURN-WINDOW-LEDGE-WEST-OFF";                      // Fensterbank westseitig
        public const string TURN_ALL_LIGHTS_WEST_OFF = "TURN-ALL-LIGHTS-WEST-OFF";
        public const string TURN_ALL_LIGHTS_WEST_ON = "TURN-ALL-LIGHTS-WEST-ON";
        public const string TURN_KITCHEN_BOARD_DOWN_LIGHTS_ON = "TURN-KITCHEN-BOARD-DOWN-LIGHTS-ON";               // Lichter unter der Blende Arbeitsfläche
        public const string TURN_KITCHEN_BOARD_DOWN_LIGHTS_OFF = "TURN-KITCHEN-BOARD-DOWN-LIGHTS-OFF";              // Lichter unter der Blende Arbeitsfläche
        public const string TURN_WINDOW_LIGHT_DOOR_ENTRY_LEFT_ON = "TURN-WINDOW-LIGHT-DOOR-ENTRY-LEFT-ON";            // Lichtbalken neben der Eingangstüre beim grossen Tür Fenster links
        public const string TURN_WINDOW_LIGHT_DOOR_ENTRY_LEFT_OFF = "TURN-WINDOW-LIGHT-DOOR-ENTRY-LEFT-OFF";           // Lichtbalken neben der Eingangstüre beim grossen Tür Fenster links
        public const string TURN_LIGHTS_WALL_WEST_ON = "TURN-LIGHTS-WALL-WEST-ON";                        // Lichterleiste auf der Wand West Seitig
        public const string TURN_LIGHTS_WALL_WEST_OFF = "TURN-LIGHTS-WALL-WEST-OFF";                       // Lichterleiste auf der Wand West Seitig
        public const string TURN_LIGHT_TRIANGLE_SMALL_WEST_ON = "TURN-LIGHT-TRIANGLE-SMALL-WEST-ON";               // Lichtleiste am Dreieckfenster West Seitig - kleine Leist
        public const string TURN_LIGHT_TRIANGLE_SMALL_WEST_OFF = "TURN-LIGHT-TRIANGLE-SMALL-WEST-OFF";              // Lichtleiste am Dreieckfenster West Seitig - kleine Leist

        public const string TURN_HEATER_BODY_EAST_ON = "TURN-HEATER-BODY-EAST-ON";                        // Heizkörper OSTEN
        public const string TURN_HEATER_BODY_EAST_OFF = "TURN-HEATER-BODY-EAST-OFF";

        public const string TURN_HEATER_BODY_WEST_ON = "TURN-HEATER-BODY-WEST-ON";                        // Heizkörper WESTEN
        public const string TURN_HEATER_BODY_WEST_OFF = "TURN-HEATER-BODY-WEST-OFF";

        public const string TURN_LIGHT_OUTSIDE_ON = "TURN-LIGHT-OUTSIDE-ON";                           // Aussenbeleuchtung
        public const string TURN_LIGHT_OUTSIDE_BY_OPEN_DOOR_CONTACT_ON = "TURN-LIGHT-OUTSIDE-BY-OPEN-DOOR-CONTACT-ON";                           // Aussenbeleuchtung
        public const string TURN_LIGHT_OUTSIDE_BY_OPEN_DOOR_CONTACT_OFF = "TURN-LIGHT-OUTSIDE-BY-OPEN-DOOR-CONTACT-OFF";                           // Aussenbeleuchtung
        public const string TURN_LIGHT_OUTSIDE_OFF = "TURN-LIGHT-OUTSIDE-OFF";

        public const string POWER_FAIL_ON = "POWER-FAIL-ON";                                   // Stromausfall
        public const string POWER_FAIL_OFF = "POWER-FAIL-OFF";                                  // Stromausfall ist wieder vorbei

        public const string TURN_LIGHT_ANTEROOM_MAIN_ON    = "TURN-LIGHT-ANTEROOM-MAIN-ON";
        public const string TURN_LIGHT_ANTEROOM_MAIN_OFF   = "TURN-LIGHT-ANTEROOM-MAIN-OFF";
        public const string TURN_LIGHT_ANTEROOM_BACK_ON    = "TURN-LIGHT-ANTEROOM-BACK-ON";
        public const string TURN_LIGHT_ANTEROOM_BACK_OFF   = "TURN-LIGHT-ANTEROOM-BACK-OFF";
        public const string TURN_LIGHT_ANTEROOM_MIDDLE_ON  = "TURN-LIGHT-ANTEROOM-MIDDLE-ON";
        public const string TURN_LIGHT_ANTEROOM_MIDDLE_OFF = "TURN-LIGHT-ANTEROOM-MIDDLE-OFF";
        public const string ACTIVATE_LIGHT_ANTEROOM_MIDDLE = "ACTIVATE-LIGHT-ANTEROOM-MIDDLE-ON";
        public const string DEACTIVE_LIGHT_ANTEROOM_MIDDLE = "DEACTIVATE-LIGHT-ANTEROOM-MIDDLE-OFF";
        public const string TURN_LIGHT_GALLERY_FLOOR_STAIRS_FRONT_ON  = "TURN-LIGHT-GALLERY-FLOOR-STAIRS-FRONT-ON";
        public const string TURN_LIGHT_GALLERY_FLOOR_STAIRS_FRONT_OFF = "TURN-LIGHT-GALLERY-FLOOR-STAIRS-FRONT-OFF";
        public const string TURN_LIGHT_GALLERY_FLOOR_STAIRS_RIGHT_ON  = "TURN-LIGHT-GALLERY-FLOOR-STAIRS-RIGHT-ON";
        public const string TURN_LIGHT_GALLERY_FLOOR_STAIRS_RIGHT_OFF = "TURN-LIGHT-GALLERY-FLOOR-STAIRS-RIGHT-OFF";
        public const string TURN_LIGHT_ANTEROOM_ON = "TURN-LIGHT-ANTEROOM-ON";
        public const string TURN_LIGHT_ANTEROOM_OFF = "TURN-LIGHT-ANTEROOM-OFF";


        static Dictionary<uint, string> ComandoDictionary = new Dictionary<uint, string>
            {
                {1,                  ON                             },
                {2,                  OFF                            },
                {4,                  BLINK                          },
                {100,                EXIT                           }
            };

        public static string GetComando( uint comandokey )
        {
            return ( GetData.ValueFromDeviceDictionary( ComandoDictionary, comandokey ) );
        }

        public static class Button
        {
            public const string Button_ = "Button";
            public const string IsPressed = "IS_PRESSED";
            public const string IsReleased = "IS_RELEASED";
            public const string Udefined = "UNDEFINED";
        }

        public static class Buttons
        {
            public const string MainDownRight = "MAIN_DOWN_RIGHT";
            public const string MainDownLeft = "MAIN_DOWN_LEFT";
            public const string MainUpLeft = "MAIN_UP_LEFT";
            public const string MainUpRight = "MAIN_UP_RIGHT";
        }

        public static class Telegram
        {
            public const string Index = "IND";
            public const string State = "STA";
            public const char Seperator = '_';
            public const int IndexTransactionCounter = 0;
            public const int IndexDigitalInputs = 1;
            public const int IndexValueDigitalInputs = 2;
        }
    }
}