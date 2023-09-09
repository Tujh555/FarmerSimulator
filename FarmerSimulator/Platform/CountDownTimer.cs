using System;
using System.Threading;

namespace FarmerSimulator.Platform;

public class CountDownTimer
{
            private readonly long _allTimeMillis;
            private readonly long _tickTimeMillis;
            private readonly Action _onStart;
            private readonly Action<long> _onTick;
            private readonly Action _onComplete;
            private volatile Thread? _thread;

            private CountDownTimer(
                long allTimeMillis,
                long tickTimeMillis,
                Action onStart,
                Action<long> onTick,
                Action onComplete)
            {
                _allTimeMillis = allTimeMillis;
                _tickTimeMillis = tickTimeMillis;
                _onStart = onStart;
                _onComplete = onComplete;
                _onTick = onTick;
            }

            public void Start()
            {
                if (_thread != null) return;

                _thread = new Thread(() =>
                {
                    _onStart();

                    for (long i = 0; i < _allTimeMillis; i += _tickTimeMillis)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(_tickTimeMillis));
                        _onTick(i);
                    }

                    _onComplete();

                    lock (this)
                    {
                        _thread = null;
                    }
                });
                
                _thread.Start();
            }

            public class Builder
            {
                private static readonly Action DefaultAction = () => { };
                private long _allTimeMillis;
                private long _tickTimeMillis;
                private Action _onStart = DefaultAction;
                private Action<long> _onTick = _ => {};
                private Action _onComplete = DefaultAction;

                public Builder AllTime(long time)
                {
                    Utils.Require(() => time >= 0 && time >= _tickTimeMillis);
                    _allTimeMillis = time;
                    return this;
                }

                public Builder TickTime(long time)
                {
                    Utils.Require(() => time >= 0 && time <= _allTimeMillis);
                    _tickTimeMillis = time;
                    return this;
                }

                public Builder OnStart(Action action)
                {
                    _onStart = action;
                    return this;
                }

                public Builder OnTick(Action<long> action)
                {
                    _onTick = action;
                    return this;
                }

                public Builder OnComplete(Action action)
                {
                    _onComplete = action;
                    return this;
                }

                public CountDownTimer Build() => new(_allTimeMillis, _tickTimeMillis, _onStart, _onTick, _onComplete);
            }
        }