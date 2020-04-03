using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    public enum State
    {
        EnterMineAndDigForNuggets,
        EnterBankAndDepositGold
    }

    public State state;

    public void Start()
    {
        state = State.EnterMineAndDigForNuggets;

        // Start the Finite State Machine
        StartCoroutine(FSM());
    }

    private IEnumerator FSM()
    {
        // Execute the current coroutine (state)
        while (true)
            yield return StartCoroutine(state.ToString());
    }

    private IEnumerator EnterMineAndDigForNuggets()
    {
        /* This part works as the Enter function
        of the previous post (it's optional) */

        print("Entering the mine...");
        yield return null;

        /* Now we enter in the Execute part of a state
        which will be usually inside a while - yield block */

        bool dig = true;
        int digged = 0;
        while (dig)
        {
            print("Digging... " + (digged++) + " " + Time.time);
            if (digged == 2) dig = false;
            yield return new WaitForSeconds(1);
        }

        /* And finally do something before leaving
        the state (optional too) and starting a new one */

        print("Exiting the mine...");
        state = State.EnterBankAndDepositGold;
    }

    private IEnumerator EnterBankAndDepositGold()
    {
        //Enter
        print("Entering the bank...");
        yield return null;

        //Execute
        bool queing = true;
        float t = Time.time;
        while (queing)
        {
            print("waiting...");
            if (Time.time - t > 5) queing = false;
            yield return new WaitForSeconds(1);
        }

        //Exit
        print("Leaving the bank a little bit richer...");
        state = State.EnterMineAndDigForNuggets;
    }
}
