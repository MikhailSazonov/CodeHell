using System;

public class Roll
{
    public static int roll() {
        Random rnd = new Random();
        return rnd.Next(0, 100);
    }

    public static bool luckyMe(int barrier) {
        int rndm = roll();
        return rndm < barrier;
    }
}
