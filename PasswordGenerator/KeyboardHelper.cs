// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.Runtime.InteropServices;
using WindowsInput.Native;

namespace PasswordGenerator
{
    public static class KeyboardHelper
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        public static bool IsKeyDown(VirtualKeyCode keyCode)
        {
            short keyState = GetAsyncKeyState((int)keyCode);
            return (keyState & 0x8000) != 0;
        }
    }
}
