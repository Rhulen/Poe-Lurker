﻿//-----------------------------------------------------------------------
// <copyright file="Skill.cs" company="Wohs Inc.">
//     Copyright © Wohs Inc.
// </copyright>
//-----------------------------------------------------------------------

namespace Lurker.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Represents a skill.
    /// </summary>
    public class Skill
    {
        #region fields

        private List<Gem> _gems;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Skill"/> class from being created.
        /// </summary>
        private Skill()
        {
            this._gems = new List<Gem>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the skills.
        /// </summary>
        public IEnumerable<Gem> Gems => this._gems;

        /// <summary>
        /// Gets or sets the slot.
        /// </summary>
        public string Slot { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Froms the XML.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="knownGems">The known gems.</param>
        /// <returns>
        /// The skill.
        /// </returns>
        public static Skill FromXml(XElement element, IEnumerable<Gem> knownGems)
        {
            var skill = new Skill()
            {
                Slot = element.Attribute("slot")?.Value,
            };

            foreach (var gemElement in element.Elements())
            {
                var gemId = gemElement.Attribute("skillId").Value;
                var gem = knownGems.FirstOrDefault(g => g.Id == gemId);
                if (gem == null)
                {
                    gem = Gem.FromXml(gemElement);
                    if (string.IsNullOrEmpty(gem.Name))
                    {
                        continue;
                    }
                }

                skill.AddGem(gem);
            }

            return skill;
        }

        /// <summary>
        /// Adds the skill.
        /// </summary>
        /// <param name="gem">The gem.</param>
        public void AddGem(Gem gem)
        {
            this._gems.Add(gem);
        }

        #endregion
    }
}