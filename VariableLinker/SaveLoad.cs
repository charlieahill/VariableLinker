﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace VariableLinker
{
    public static class SaveLoad
    {
        /// <summary>
        /// Saves the list of notes to file
        /// </summary>
        /// <param name="notes">The list of note models to be saved</param>
        /// <param name="notFilename">The full filepath of the file containing these notes</param>
        public static void Save(this BindingList<WebsiteModel> notes, string notFilename)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(notFilename)))
                    Directory.CreateDirectory(Path.GetDirectoryName(notFilename));

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(BindingList<WebsiteModel>));
                using (StreamWriter writer = new StreamWriter(notFilename))
                    xmlSerializer.Serialize(writer, notes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save website data. Changes to the day data made in this session will be lost." + Environment.NewLine + Environment.NewLine + ex.ToString());
            }
        }

        /// <summary>
        /// Loads notes data from file
        /// </summary>
        /// <param name="notFilename">The filename of the file to load</param>
        /// <returns>The loaded program data</returns>
        public static BindingList<WebsiteModel> Load(string notFilename)
        {
            try
            {
                if (!File.Exists(notFilename)) return new BindingList<WebsiteModel>();

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(BindingList<WebsiteModel>));
                using (StreamReader reader = new StreamReader(notFilename))
                    return (BindingList<WebsiteModel>)xmlSerializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load website data. A blank data model will be loaded in for this session." + Environment.NewLine + Environment.NewLine + ex.ToString());
                return new BindingList<WebsiteModel>();
            }
        }
    }
}
