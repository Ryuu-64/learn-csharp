using System.Runtime.CompilerServices;

namespace LearnCSharp;

public static class LearnTaskStateMachine
{
    private struct FooStateMachine : IAsyncStateMachine
    {
        public AsyncTaskMethodBuilder Builder
        {
            get => _builder;
            private init => _builder = value;
        }

        /// <summary>
        /// -1 初始化状态
        /// 0,1,2,... 对应每个 await 恢复点
        /// -2 完成状态
        /// </summary>
        private int _state;

        private AsyncTaskMethodBuilder _builder;
        private TaskAwaiter<int> awaiter3;

        public FooStateMachine()
        {
            Builder = AsyncTaskMethodBuilder.Create();
            _state = -1;
        }

        public void MoveNext()
        {
            Console.WriteLine("MoveNext called with state: " + _state);
            try
            {
                switch (_state)
                {
                    case -1:
                        TaskAwaiter awaiter1 = Task.CompletedTask.GetAwaiter();
                        Console.WriteLine("1");
                        if (!awaiter1.IsCompleted)
                        {
                            _state = 0;
                            _builder.AwaitUnsafeOnCompleted(ref awaiter1, ref this);
                            return;
                        }

                        awaiter1.GetResult();
                        _state = 0;
                        goto case 0;
                    case 0:
                        TaskAwaiter awaiter2 = Task.Delay(1000).GetAwaiter();
                        Console.WriteLine("2");
                        if (!awaiter2.IsCompleted)
                        {
                            _state = 1;
                            _builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
                            return;
                        }

                        awaiter2.GetResult();
                        _state = 1;
                        goto case 1;
                    case 1:
                        awaiter3 = Task.Run(() => 42).GetAwaiter();
                        Console.WriteLine("3");
                        if (!awaiter3.IsCompleted)
                        {
                            _state = 2;
                            _builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
                            return;
                        }

                        break;
                    case 2:
                        int result = awaiter3.GetResult();
                        Console.WriteLine("Result: " + result);
                        break;
                }

                _builder.SetResult();
                _state = -2;
                Console.WriteLine("Inside Foo (handwritten state machine)");
            }
            catch (Exception ex)
            {
                _builder.SetException(ex);
                _state = -2;
            }
        }

        public void SetStateMachine(IAsyncStateMachine sm)
        {
        }
    }

    public static async Task StartAsync()
    {
        Console.WriteLine("Before Foo");
        var stateMachine = new FooStateMachine();
        stateMachine.Builder.Start(ref stateMachine);
        await stateMachine.Builder.Task;
        Console.WriteLine("After Foo");
    }
}