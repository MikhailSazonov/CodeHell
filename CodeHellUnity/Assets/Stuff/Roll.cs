using System;

public class Roll
{
    public static int roll(int low = 0, int high = 100) {
        Random rnd = new Random();
        return rnd.Next(low, high);
    }

    public static bool luckyMe(int barrier) {
        int rndm = roll();
        return rndm < barrier;
    }
}
