﻿namespace Terrain
{
    public class ScalarField
    {
        public float[] data;
        protected int nx, ny;
        
        public int NX
        {
            get => nx;
        }

        public int NY
        {
            get => ny;
        }

        public ScalarField(int nx, int ny)
        {
            this.nx = nx;
            this.ny = ny;
            data = new float[nx * ny];
        }

        public void SetData(float[] data)
        {
            this.data = data;
        }
    }
}