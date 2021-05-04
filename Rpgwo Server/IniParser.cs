using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Rpgwo_Server
{

    public class IniParser : IDisposable
    {
        private StreamReader _streamReader;
        private string _delimiter;
        private string _lastLine = null;
        List<IniEntry> _currentSet = new List<IniEntry>();

        public IniParser(string fileLocation, string delimiter)
        {
            _streamReader = new StreamReader(fileLocation);
            _delimiter = delimiter.ToLower();
        }

        public List<IniEntry> NextEntry()
        {
            return Parse();
        }

        private List<IniEntry> Parse()
        {
            try
            {
                while ((_lastLine = _streamReader.ReadLine()) != null)
                {
                    // Check to see if the line is a comment.
                    if (_lastLine == "" || _lastLine[0] == ';')
                        continue;

                    // Start of a new entry.
                    if (_lastLine.ToLower().StartsWith(_delimiter))
                    {
                        var tmp = _currentSet;
                        _currentSet = new List<IniEntry>();
                        var entry = ParseLine(_lastLine);
                        _currentSet.Add(entry);

                        return tmp;
                    }
                    else
                    {
                        var entry = ParseLine(_lastLine);
                        _currentSet.Add(entry);
                    }
                }

                var tmp2 = _currentSet;
                _currentSet = null;

                return tmp2;
            } 
            catch (Exception e)
            {
                // For now, just quietly eat the exception. TODO :: Possibly reconsider this.
                return null;
            }
        }

        private IniEntry ParseLine(string line)
        {
            IniEntry iniEntry = null;

            // Strip out any comments at the end of the line.
            var semiIndex = line.IndexOf(';');

            if (semiIndex > -1)
            {
                line = line.Substring(0, semiIndex);
            }

            // Just to be sure, clean any white space.
            line = line.Trim();

            if (line.Contains('='))
            {
                var sp = line.Split('=');

                if (sp.Length >= 2)
                {
                    iniEntry = new IniEntry(sp[0].ToLower(), sp[1]);
                }
            }
            else
            {
                iniEntry = new IniEntry(line.ToLower());
            }

            return iniEntry;
        }

        public void Dispose()
        {
            if (_streamReader != null)
            {
                _streamReader.Close();
                _streamReader = null;
            }
        }   
    }

    public class IniEntry
    {
        private readonly string _key;
        public string Key => _key;

        private readonly string _value = null;
        public string Value => _value;

        public IniEntry(string key, string value)
        {
            _key = key;
            _value = value;
        }

        public IniEntry(string key)
        {
            _key = key;
        }

        public int ValueAsInt()
        {
            int i;

            try
            {
                i = Convert.ToInt32(_value);

                return i;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
    