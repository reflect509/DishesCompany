namespace SF2022User_NN_Lib
{
    public class Calculations
    {
        public Calculations() { }

        public string[] AvailablePeriods(
            TimeSpan[] startTimes,
            int[] durations,
            TimeSpan beginWorkingTime,
            TimeSpan endWorkingTime,
            int consultationTime
            )
        {
            if (consultationTime < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(consultationTime));
            }
            for (int i = 0; i < durations.Length; i++)
            {
                if (durations[i] < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(durations));
                }
            }
            int j = 0;
            List<string> availablePeriods = new List<string>();

            if (beginWorkingTime >= endWorkingTime)
            {
                return availablePeriods.ToArray();
            }
            else
            {
                while (beginWorkingTime < endWorkingTime)
                {
                    TimeSpan ConsulationEndTime = beginWorkingTime + TimeSpan.FromMinutes(consultationTime);
                    if (ConsulationEndTime > endWorkingTime)
                    {
                        break;
                    }
                    else
                    {
                        if (j == durations.Length || ConsulationEndTime <= startTimes[j])
                        {
                            availablePeriods.Add($"{string.Format("{0:hh\\:mm}", beginWorkingTime)}-{string.Format("{0:hh\\:mm}", ConsulationEndTime)}");
                            beginWorkingTime = ConsulationEndTime;
                        }
                        else
                        {
                            beginWorkingTime = startTimes[j] + TimeSpan.FromMinutes(durations[j]);
                            j++;
                        }
                    }
                }
                return availablePeriods.ToArray();
            }
        }
    }
}
