﻿using StbTrueTypeSharp;

namespace FontStash.NET
{
    internal static class FonsTt
    {
        public static int LoadFont(FonsTtImpl font, byte[] data, int fontIndex)
        {
            font.Font = new FontInfo();
            var offset = Common.stbtt_GetFontOffsetForIndex(data, fontIndex);
            var stbError = offset == FontManager.Invalid ? 0 : font.Font.stbtt_InitFont(data, offset);

            return stbError;
        }

        public static void GetFontVMetrics(FonsTtImpl font, out int ascent, out int descent, out int linegap)
        {
            font.Font.stbtt_GetFontVMetrics(out ascent, out descent, out linegap);
        }

        public static float GetPixelHeightScale(FonsTtImpl font, float size)
        {
            return font.Font.stbtt_ScaleForMappingEmToPixels(size);
        }

        public static int GetGlyphIndex(FonsTtImpl font, int codepoint)
        {
            return font.Font.stbtt_FindGlyphIndex(codepoint);
        }

        public static bool BuildGlyphBitmap(FonsTtImpl font, int glyph, float scale, out int advance, out int lsb,
            out int x0, out int y0, out int x1, out int y1)
        {
            advance = 0;
            lsb = 0;
            x0 = y0 = x1 = y1 = 0;
            font.Font.stbtt_GetGlyphHMetrics(glyph, ref advance, ref lsb);
            font.Font.stbtt_GetGlyphBitmapBox(glyph, scale, scale, ref x0, ref y0, ref x1, ref y1);
            return true;
        }

        public static void RenderGlyphBitmap(FonsTtImpl font, byte[] texData, int startIndex, int outWidth,
            int outHeight, int outStride, float scaleX, float scaleY, int glyph)
        {
            FakePtr<byte> ptr = new(texData, startIndex);
            font.Font.stbtt_MakeGlyphBitmap(ptr, outWidth, outHeight, outStride, scaleX, scaleY, glyph);
        }

        public static int GetGlyphKernAdvance(FonsTtImpl font, int glyph1, int glyph2)
        {
            return font.Font.stbtt_GetGlyphKernAdvance(glyph1, glyph2);
        }
    }
}