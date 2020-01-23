using System.Collections;
using System.Collections.Generic;
using Android.Text.Method;
using Android.Views;
using Java.Lang;

namespace Ru.Tattelecom.SmartIntercom.Utilits
{
    public class AsteriskPasswordTransformationMethod : PasswordTransformationMethod, ICharSequence
    {
        private ICharSequence _sequence;

        public IEnumerator<char> GetEnumerator()
        {
            return _sequence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public char CharAt(int index)
        {
            return '*';
        }

        public int Length()
        {
            return _sequence.Length();
        }

        public ICharSequence SubSequenceFormatted(int start, int end)
        {
            return _sequence.SubSequenceFormatted(start, end);
        }

        public override ICharSequence GetTransformationFormatted(ICharSequence source, View view)
        {
            _sequence = source;
            return this;
        }
    }
}