﻿using CKK.Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKK.Logic.Interfaces
{
    [Serializable]
    public abstract class Entity
    {
        private int id;
        private string name;

        public int Id {
            get
            {
                return id;
            }
            set
            {
                if (value < 0)
                {
                    throw new InvalidIdException();
                }

                id = value;
            }
        }
        public string Name { get; set; }
    }
}
