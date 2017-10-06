using System;
namespace TelegrEvaluator_UnitTests
{
    public static class VerificationTelegramms
    {
        public static string Telegramm_1        = "Schlafzimmer_FENSTERINFORMATION_FensterWestSeite_OFFEN_15052017:20h16m33s520ms_MansardenFensterLinkeSeite_GESCHLOSSEN_19122018:21h29m44s333ms";
        public static string Telegramm_2        = "Schlafzimmer_FENSTERINFORMATION_FensterWestSeite_GESCHLOSSEN_19122018:21h29m44s333ms_MansardenFensterLinkeSeite_OFFEN_19122018:21h35m44s333ms";
        public static string TelegrammAllClosed = "Schlafzimmer_FENSTERINFORMATION_FensterWestSeite_GESCHLOSSEN_19122018:21h29m44s333ms_MansardenFensterLinkeSeite_GESCHLOSSEN_19122018:21h35m44s333ms";
        public static string TelegrammUnknown   = "Schlafzimmer_FENSTERINFORMATION_FensterWestSeite_UNBEKANNT_19122018:21h29m44s333ms_MansardenFensterLinkeSeite_OFFEN_19122018:21h35m44s333ms";
        public static string TelegrammDoor      = "Wohnzimmer_TÜRINFORMATION_TürRechts_OFFEN_19122018:21h29m44s333ms";

    }
}
