
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Godot;
using Godot.Collections;

namespace rosthouse.sharpest.addon.utils;

public static class TextGenerator
{

  private static Array<string> words = [
  "lorem",
  "ipsum",
  "amet",
  "pellentesque",
  "mattis",
  "accumsan",
  "maximus",
  "etiam",
  "mollis",
  "ligula",
  "non",
  "iaculis",
  "ornare",
  "mauris",
  "efficitur",
  "ex",
  "eu",
  "rhoncus",
  "aliquam",
  "in",
  "hac",
  "habitasse",
  "platea",
  "dictumst",
  "maecenas",
  "ultrices",
  "purus",
  "at",
  "venenatis",
  "auctor",
  "sem",
  "nulla",
  "urna",
  "molestie",
  "nisi",
  "mi",
  "a",
  "ut",
  "euismod",
  "nibh",
  "id",
  "libero",
  "lacinia",
  "sit",
  "amet",
  "lacinia",
  "lectus",
  "viverra",
  "donec",
  "scelerisque",
  "dictum",
  "enim",
  "dignissim",
  "dolor",
  "cursus",
  "morbi",
  "rhoncus",
  "elementum",
  "magna",
  "sed",
  "sed",
  "velit",
  "consectetur",
  "adipiscing",
  "elit",
  "curabitur",
  "nulla",
  "eleifend",
  "vel",
  "tempor",
  "metus",
  "phasellus",
  "vel",
  "pulvinar",
  "lobortis",
  "quis",
  "nullam",
  "felis",
  "orci",
  "congue",
  "vitae",
  "augue",
  "nisi",
  "tincidunt",
  "id",
  "posuere",
  "fermentum",
  "facilisis",
  "ultricies",
  "mi",
  "nisl",
  "fusce",
  "neque",
  "vulputate",
  "integer",
  "tortor",
  "tempus",
  "praesent",
  "proin",
  "quis",
  "nunc",
  "massa",
  "congue",
  "quam",
  "auctor",
  "eros",
  "placerat",
  "eros",
  "leo",
  "nec",
  "sapien",
  "egestas",
  "duis",
  "feugiat",
  "vestibulum",
  "porttitor",
  "odio",
  "sollicitudin",
  "arcu",
  "et",
  "aenean",
  "sagittis",
  "ante",
  "urna",
  "fringilla",
  "risus",
  "et",
  "vivamus",
  "semper",
  "nibh",
  "eget",
  "finibus",
  "est",
  "laoreet",
  "justo",
  "commodo",
  "sagittis",
  "vitae",
  "nunc",
  "diam",
  "ac",
  "tellus",
  "posuere",
  "condimentum",
  "enim",
  "tellus",
  "faucibus",
  "suscipit",
  "ac",
  "nec",
  "turpis",
  "interdum",
  "malesuada",
  "fames",
  "primis",
  "quisque",
  "pretium",
  "ex",
  "feugiat",
  "porttitor",
  "massa",
  "vehicula",
  "dapibus",
  "blandit",
  "hendrerit",
  "elit",
  "aliquet",
  "nam",
  "orci",
  "fringilla",
  "blandit",
  "ullamcorper",
  "mauris",
  "ultrices",
  "consequat",
  "tempor",
  "convallis",
  "gravida",
  "sodales",
  "volutpat",
  "finibus",
  "neque",
  "pulvinar",
  "varius",
  "porta",
  "laoreet",
  "eu",
  "ligula",
  "porta",
  "placerat",
  "lacus",
  "pharetra",
  "erat",
  "bibendum",
  "leo",
  "tristique",
  "cras",
  "rutrum",
  "at",
  "dui",
  "tortor",
  "in",
  "varius",
  "arcu",
  "interdum",
  "vestibulum",
  "magna",
  "ante",
  "imperdiet",
  "erat",
  "luctus",
  "odio",
  "non",
  "dui",
  "volutpat",
  "bibendum",
  "quam",
  "euismod",
  "mattis",
  "class",
  "aptent",
  "taciti",
  "sociosqu",
  "ad",
  "litora",
  "torquent",
  "per",
  "conubia",
  "nostra",
  "inceptos",
  "himenaeos",
  "suspendisse",
  "lorem",
  "a",
  "sem",
  "eleifend",
  "commodo",
  "dolor",
  "cursus",
  "luctus",
  "lectus"
  ];

  private static int currentWord = 0;

  public static string GetWord()
  {
    string word = words[currentWord];
    currentWord = Mathf.Wrap(currentWord + 1, 0, words.Count());
    return word;
  }

  public static string GetSentence(int words, bool withPunctuation = false)
  {
    StringBuilder builder = new StringBuilder();
    for (int i = 0; i < words; i++)
    {
      var word = GetWord();
      if (i == 0)
      {
        word = word.Capitalize();
      }
      builder.Append(word);
      if (withPunctuation)
      {
        if (GD.Randf() < 0.3)
        {
          builder.Append(",");
        }
      }

      builder.Append(" ");
    }

    if (withPunctuation)
    {
      builder.Append(".");
    }
    return builder.ToString();
  }

  public static string GetParagraph(int sentences, int minWords = 1, int maxWords = 20)
  {
    StringBuilder builder = new StringBuilder();
    for (int i = 0; i < sentences; i++)
    {
      var sentence = GetSentence(Mathf.FloorToInt(Mathf.Lerp(minWords, maxWords, GD.Randf())));
      builder.Append(sentence);
      if (i < sentences - 1)
      {
        builder.Append(" ");
      }
    }
    return builder.ToString();
  }

  public static void Reset() => currentWord = 0;

}
