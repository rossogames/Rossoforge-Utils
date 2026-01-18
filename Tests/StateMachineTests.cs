using NUnit.Framework;
using Rossoforge.Utils.StateMachine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rossoforge.Utils.Tests
{
    public class StateMachineTests
    {
        private TestStateMachine machine;
        private FakeState stateA;
        private FakeState stateB;

        [SetUp]
        public void Setup()
        {
            machine = new TestStateMachine();
            stateA = new FakeStateA();
            stateB = new FakeStateB();
        }

        [Test]
        public void StartMachine_ShouldSetInitialState()
        {
            machine.StartMachine(stateA);

            Assert.AreEqual(stateA, machine.CurrentState);
            Assert.IsTrue(stateA.EnterCalled);
        }

        [Test]
        public void StartMachine_WithNull_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() => machine.StartMachine(null));
        }

        [Test]
        public void TransitionTo_SameState_ShouldReturnFalse()
        {
            machine.StartMachine(stateA);

            var result = machine.TransitionTo(stateA).GetAwaiter().GetResult();

            Assert.IsFalse(result);
            Assert.AreEqual(stateA, machine.CurrentState);
        }

        [Test]
        public void TransitionTo_BeforeStart_ShouldReturnFalse()
        {
            var result = machine.TransitionTo(stateA).GetAwaiter().GetResult();

            Assert.IsFalse(result);
        }

        [Test]
        public void TransitionToPreviousState_WithNoPrevious_ShouldReturnFalse()
        {
            machine.StartMachine(stateA);

            var result = machine.TransitionToPreviousState().GetAwaiter().GetResult();

            Assert.IsFalse(result);
        }

        [Test]
        public void Update_ShouldCallCurrentStateUpdate()
        {
            machine.StartMachine(stateA);

            machine.Update();

            Assert.IsTrue(stateA.UpdateCalled);
        }

        [Test]
        public void Update_ShouldNotRunDuringTransition()
        {
            machine.StartMachine(stateA);

            // Simulamos estado de transición
            typeof(StateMachine<FakeState>)
                .GetProperty("IsTransitionInProgress")
                .SetValue(machine, true);

            machine.Update();

            Assert.IsFalse(stateA.UpdateCalled);
        }

        [Test]
        public void StartMachine_Twice_ShouldNotOverrideCurrentState()
        {
            machine.StartMachine(stateA);
            machine.StartMachine(stateB);

            Assert.AreEqual(stateA, machine.CurrentState);
        }

        [Test]
        public void GetPreviousStateType_ShouldReturnCorrectType()
        {
            machine.StartMachine(stateA);
            machine.TransitionTo(stateB).GetAwaiter().GetResult();

            var type = machine.GetPreviousStateType();

            Assert.AreEqual(typeof(FakeStateA), type);
        }
    }

    public class TestStateMachine : StateMachine<FakeState>
    {
    }

    public class FakeState : IState
    {
        public bool EnterCalled { get; private set; }
        public bool ExitCalled { get; private set; }
        public bool UpdateCalled { get; private set; }

        public List<string> CallOrder = new();

        public void Enter()
        {
            EnterCalled = true;
            CallOrder.Add("Enter");
        }

        public void Exit()
        {
            ExitCalled = true;
            CallOrder.Add("Exit");
        }

        public void Update()
        {
            UpdateCalled = true;
            CallOrder.Add("Update");
        }

        public void Reset()
        {
            EnterCalled = ExitCalled = UpdateCalled = false;
            CallOrder.Clear();
        }
    }
    public class FakeStateA : FakeState
    {
    }

    public class FakeStateB : FakeState
    {
    }
}
