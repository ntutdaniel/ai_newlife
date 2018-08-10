using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Drawing;
using System.Reflection;

namespace Face_lock
{
    public class ExFace
    {
        public const string UNKNOWN = "unknow";
        Face _face;
        string _personName = UNKNOWN;
        List<Point> _landmarkPointList;

        public ExFace(Face face)
        {
            _landmarkPointList = new List<Point>();
            _face = face;
            var landmark = face.FaceLandmarks;
            foreach (PropertyInfo propertyInfo in landmark.GetType().GetProperties())
            {
                FeatureCoordinate coordinate = propertyInfo.GetValue(face.FaceLandmarks) as FeatureCoordinate;
                //Console.WriteLine("X = {0},  Y = {1}", coordinate.X, coordinate.Y);
                //Console.WriteLine("---------------------" + propertyInfo.GetValue(landmark));
                _landmarkPointList.Add(new Point((int)coordinate.X,(int)coordinate.Y));
            }
        }

        public Face Face
        {
            get
            {
                return _face;
            }
        }

        public string PersonName
        {
            get
            {
                return _personName;
            }
            set
            {
                _personName = value;
            }
        }
        
        public List<Point> LandmarkPointList
        {
            get
            {
                return _landmarkPointList;
            }
        }
        
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle()
                {
                    X = _face.FaceRectangle.Left,
                    Y = _face.FaceRectangle.Top,
                    Width = _face.FaceRectangle.Width,
                    Height = _face.FaceRectangle.Height
                };
            }
        }
    }
}