namespace MovieWebApp.Utility
{
    public static class Utils
    {
        public static string TimeFormat(int secs)
        {
            string answer;
            TimeSpan t = TimeSpan.FromSeconds(secs);

            if (t.Hours != 0)
            {
                answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                           t.Hours,
                           t.Minutes,
                           t.Seconds);
            }
            else
            {
                answer = string.Format("{0:D2}:{1:D2}",
                           t.Minutes,
                           t.Seconds);
            }
            return answer;
        }
    }
}
