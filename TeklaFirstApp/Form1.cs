using System;
using System.Collections;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Drawing;
using Tekla.Structures.Model.UI;

namespace TeklaFirstApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Model Model = new Model();
        private DrawingHandler DrawingHandler = new DrawingHandler();

        #region Coordinate system calculations
        readonly Tekla.Structures.Geometry3d.Vector UpDirection = new Tekla.Structures.Geometry3d.Vector(0.0, 0.0, 1.0);

        private Tekla.Structures.Geometry3d.CoordinateSystem GetBasicViewsCoordinateSystemForFrontView(Tekla.Structures.Geometry3d.CoordinateSystem objectCoordinateSystem)
        {
            Tekla.Structures.Geometry3d.CoordinateSystem result = new Tekla.Structures.Geometry3d.CoordinateSystem();

            result.Origin = new Tekla.Structures.Geometry3d.Point(objectCoordinateSystem.Origin);
            result.AxisX = new Tekla.Structures.Geometry3d.Vector(objectCoordinateSystem.AxisX) * -1.0;

            result.AxisY = new Tekla.Structures.Geometry3d.Vector(objectCoordinateSystem.AxisY);
            Tekla.Structures.Geometry3d.Vector tempVector = (result.AxisX.Cross(UpDirection));

            if (tempVector == new Tekla.Structures.Geometry3d.Vector())
                tempVector = (objectCoordinateSystem.AxisY.Cross(UpDirection));

            result.AxisX = tempVector.Cross(UpDirection).GetNormal();
            result.AxisY = UpDirection.GetNormal();
            return result;
        }

        private Tekla.Structures.Geometry3d.CoordinateSystem GetBasicViewsCoordinateSystemForTopView(Tekla.Structures.Geometry3d.CoordinateSystem objectCoordinateSystem)
        {
            Tekla.Structures.Geometry3d.CoordinateSystem result = new Tekla.Structures.Geometry3d.CoordinateSystem();

            result.Origin = new Tekla.Structures.Geometry3d.Point(objectCoordinateSystem.Origin);
            result.AxisX = new Tekla.Structures.Geometry3d.Vector(objectCoordinateSystem.AxisX) * -1.0;

            result.AxisY = new Tekla.Structures.Geometry3d.Vector(objectCoordinateSystem.AxisY);
            Tekla.Structures.Geometry3d.Vector tempVector = (result.AxisX.Cross(UpDirection));

            if (tempVector == new Tekla.Structures.Geometry3d.Vector())
                tempVector = (objectCoordinateSystem.AxisY.Cross(UpDirection));

            result.AxisX = tempVector.Cross(UpDirection);
            result.AxisY = tempVector;
            return result;
        }

        private Tekla.Structures.Geometry3d.CoordinateSystem GetBasicViewsCoordinateSystemForEndView(Tekla.Structures.Geometry3d.CoordinateSystem objectCoordinateSystem)
        {
            Tekla.Structures.Geometry3d.CoordinateSystem result = new Tekla.Structures.Geometry3d.CoordinateSystem();

            result.Origin = new Tekla.Structures.Geometry3d.Point(objectCoordinateSystem.Origin);
            result.AxisX = new Tekla.Structures.Geometry3d.Vector(objectCoordinateSystem.AxisX) * -1.0;

            result.AxisY = new Tekla.Structures.Geometry3d.Vector(objectCoordinateSystem.AxisY);
            Tekla.Structures.Geometry3d.Vector tempVector = (result.AxisX.Cross(UpDirection));

            if (tempVector == new Tekla.Structures.Geometry3d.Vector())
                tempVector = (objectCoordinateSystem.AxisY.Cross(UpDirection));

            result.AxisX = tempVector;
            result.AxisY = UpDirection;
            return result;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            TransformationPlane current = Model.GetWorkPlaneHandler().GetCurrentTransformationPlane(); //Глобальная плоскость

            try
            {
                Model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane());
                ModelObjectEnumerator selectedModelObjects = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();

                GADrawing MyDrawing = null;
                while (selectedModelObjects.MoveNext())
                {
                    Tekla.Structures.Geometry3d.CoordinateSystem ModelObjectCoordSys;
                    string ModelObjectName;

                    GetCoordinateSystemAndNameOfSelectedObject(selectedModelObjects, out ModelObjectCoordSys, out ModelObjectName);

                    //создание нового общего чертежа
                    string DrawingName = "PartBasicViews" + (selectedModelObjects.Current as Tekla.Structures.Model.ModelObject).Identifier;
                    MyDrawing = new GADrawing(DrawingName, "standard");
                    MyDrawing.Insert();

                    if (openDrawings.Checked)
                        DrawingHandler.SetActiveDrawing(MyDrawing, true); //Открытие чертежа в редакторе
                    else
                        DrawingHandler.SetActiveDrawing(MyDrawing, false); //Открытие чертежа в невидимом режиме

                    ArrayList Parts = new ArrayList();

                    if (selectedModelObjects.Current is Tekla.Structures.Model.Part)
                        Parts.Add(selectedModelObjects.Current.Identifier);
                    else if (selectedModelObjects.Current is Tekla.Structures.Model.Assembly)
                        Parts = GetAssemblyParts(selectedModelObjects.Current as Tekla.Structures.Model.Assembly);
                    else if (selectedModelObjects.Current is Tekla.Structures.Model.BaseComponent)
                        Parts = GetComponentParts(selectedModelObjects.Current as Tekla.Structures.Model.BaseComponent);

                }
            }
            catch (Exception exception)
            {
                Model.GetWorkPlaneHandler().SetCurrentTransformationPlane(current);
                MessageBox.Show(exception.ToString());
            }
        }

        private static void GetCoordinateSystemAndNameOfSelectedObject(ModelObjectEnumerator SelectedModelObjects, out Tekla.Structures.Geometry3d.CoordinateSystem ModelObjectCoordSys, out string ModelObjectName)
        {
            if (SelectedModelObjects.Current is Tekla.Structures.Model.Part)
            {
                ModelObjectCoordSys = (SelectedModelObjects.Current as Tekla.Structures.Model.Part)
                    .GetCoordinateSystem();
                ModelObjectName = (SelectedModelObjects.Current as Tekla.Structures.Model.Part)
                    .GetPartMark();
            }
            else if (SelectedModelObjects.Current is Tekla.Structures.Model.Assembly)
            {
                ModelObjectCoordSys = (SelectedModelObjects.Current as Tekla.Structures.Model.Assembly)
                   .GetMainPart().GetCoordinateSystem();
                ModelObjectName = (SelectedModelObjects.Current as Tekla.Structures.Model.Assembly)
                    .AssemblyNumber.Prefix +
                    (SelectedModelObjects.Current as Tekla.Structures.Model.Assembly).AssemblyNumber.StartNumber;
            }
            else if (SelectedModelObjects.Current is Tekla.Structures.Model.BaseComponent)
            {
                ModelObjectCoordSys = (SelectedModelObjects.Current as Tekla.Structures.Model.BaseComponent)
                    .GetCoordinateSystem();
                ModelObjectName = (SelectedModelObjects.Current as Tekla.Structures.Model.BaseComponent)
                    .Name;
            }
            else
            {
                ModelObjectCoordSys = new Tekla.Structures.Geometry3d.CoordinateSystem();
                ModelObjectName = "";
            }
        }

        #region Model object part fetching
        private static ArrayList GetAssemblyParts(Assembly assembly)
        {
            ArrayList Parts = new ArrayList();
            IEnumerator AssemblyChildren = (assembly).GetSecondaries().GetEnumerator();
            Parts.Add((assembly).GetMainPart().Identifier);
            while (AssemblyChildren.MoveNext())
                Parts.Add((AssemblyChildren.Current as Tekla.Structures.Model.ModelObject).Identifier);

            return Parts;
        }

        private static ArrayList GetComponentParts(BaseComponent component)
        {
            ArrayList Parts = new ArrayList();
            IEnumerator MyChildren = component.GetChildren();
            while (MyChildren.MoveNext())
                Parts.Add((MyChildren.Current as Tekla.Structures.Model.ModelObject).Identifier);

            return Parts;
        }

        #endregion 

        private void CreateViews(Tekla.Structures.Geometry3d.CoordinateSystem ModelObjectCoordSys, string ModelObjectName, GADrawing MyDrawing, ArrayList Parts)
        {
            if (createFrontView.Checked)
                AddViews("Front View of" + ModelObjectName, MyDrawing, Parts, GetBasicViewsCoordinateSystemForFrontView(ModelObjectCoordSys));
            if (createTopView.Checked)
                AddViews("Top View of" + ModelObjectName, MyDrawing, Parts, GetBasicViewsCoordinateSystemForTopView(ModelObjectCoordSys));
            if (createEndView.Checked)
                AddViews("End View of" + ModelObjectName, MyDrawing, Parts, GetBasicViewsCoordinateSystemForEndView(ModelObjectCoordSys));
            if (create3dView.Checked)
                AddViews("3d View of" + ModelObjectName, MyDrawing, Parts, GetBasicViewsCoordinateSystemFor3dView(ModelObjectCoordSys));
        }

        private void AddViews(String Name, Drawing MyDrawing, ArrayList Parts, Tekla.Structures.Geometry3d.CoordinateSystem coordinateSystem)
        {
            Tekla.Structures.Drawing.View MyView = new Tekla.Structures.Drawing.View(MyDrawing.GetSheet(), coordinateSystem, coordinateSystem, Parts);
            MyView.Name = Name;
            MyView.Insert();
        }

        private void AddRotatedView(String Name, Drawing MyDrawing, ArrayList Parts, Tekla.Structures.Geometry3d.CoordinateSystem coordinateSystem)
        {
            Tekla.Structures.Geometry3d.CoordinateSystem displayCoordinateSystem = new Tekla.Structures.Geometry3d.CoordinateSystem();
            Tekla.Structures.Geometry3d.Matrix AroundX = Tekla.Structures.Geometry3d.MatrixFactory.Rotate(20.0 * Math.PI * 2.0 / 360.0, coordinateSystem.AxisX);
            Tekla.Structures.Geometry3d.Matrix AroundZ = Tekla.Structures.Geometry3d.MatrixFactory.Rotate(30.0 * Math.PI * 2.0 / 360.0, coordinateSystem.AxisY);

            Tekla.Structures.Geometry3d.Matrix Rotation = AroundX * AroundZ;
        }
    }
}
