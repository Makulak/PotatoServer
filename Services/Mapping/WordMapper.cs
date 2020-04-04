using PotatoServer.Database.Models.CleverWord;
using PotatoServer.ViewModels.CleverWord.Word;
using System.Collections.Generic;
using System.Linq;

namespace PotatoServer.Services.Mapping
{
    public class WordMapper
    {
        public Word MapToWord(WordPostVm wordVm)
        {
            if (wordVm == null)
                return null;

            return new Word
            {
                Name = wordVm.Name,
                Definition = wordVm.Definition
            };
        }

        public IEnumerable<Word> MapToWord(IEnumerable<WordPostVm> wordsVm)
        {
            if (wordsVm == null)
                return null;

            return wordsVm.Select(word => MapToWord(word));
        }

        public WordGetVm MapToWordGetVm(Word word)
        {
            if (word == null)
                return null;

            return new WordGetVm
            {
                Id = word.Id,
                Name = word.Name,
                Definition = word.Definition
            };
        }

        public IEnumerable<WordGetVm> MapToWordGetVm(IEnumerable<Word> words)
        {
            if (words == null)
                return null;

            return words.Select(word => MapToWordGetVm(word));
        }
    }
}
