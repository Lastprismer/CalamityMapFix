using CalamityMod.World;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMapFix
{
    public class CalamityMapFix : Mod
    {
        private CalamityMapFix instance;
        private static ILHook ilHook;
        public CalamityMapFix Instance
        {
            get => instance;
        }
        public override void Load()
        {
            instance = this;
            MonoModHooks.RequestNativeAccess();
            ilHook = new ILHook(typeof(MiscWorldgenRoutines).GetMethod("GenerateBiomeChests", (BindingFlags)60),
                il =>
                {
                    var cursor = new ILCursor(il);

                    if (!cursor.TryGotoNext(i => i.MatchLdstr("dMinX")))
                        return;
                    cursor.Index += 2;
                    cursor.Emit(OpCodes.Pop);
                    cursor.Emit(OpCodes.Ldc_I4, 60);

                    if (!cursor.TryGotoNext(i => i.MatchLdstr("dMaxX")))
                        return;
                    cursor.Index += 2;
                    cursor.Emit(OpCodes.Pop);
                    cursor.Emit(OpCodes.Ldc_I4, 60);

                    if (!cursor.TryGotoNext(i => i.MatchLdstr("dMaxY")))
                        return;
                    cursor.Index += 2;
                    cursor.Emit(OpCodes.Pop);
                    cursor.Emit(OpCodes.Ldc_I4, 60);
                });
            ilHook.Apply();
        }
        public override void Unload()
        {
            if (ilHook != null)
                ilHook.Dispose();
            ilHook = null;
            instance = null;
        }
    }
}