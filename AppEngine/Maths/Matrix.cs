namespace Maths
{
	public struct Matrix {
		public float m11, m12, m13, m14;
		public float m21, m22, m23, m24;
		public float m31, m32, m33, m34;
		public float m41, m42, m43, m44;

		// generated using `ctorf`:
		public Matrix(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44) {
			this.m11 = m11; this.m12 = m12; this.m13 = m13; this.m14 = m14;
			this.m21 = m21; this.m22 = m22; this.m23 = m23; this.m24 = m24;
			this.m31 = m31; this.m32 = m32; this.m33 = m33; this.m34 = m34;
			this.m41 = m41; this.m42 = m42; this.m43 = m43; this.m44 = m44;
		}

		public static Matrix Identity => new Matrix
		  ( 1, 0, 0, 0,
			0, 1, 0, 0,
			0, 0, 1, 0,
			0, 0, 0, 1);

		public static Vector operator *(Matrix m, Vector v) {
			return new Vector(m.m11 * v.X + m.m12 * v.Y + m.m13 * v.Z + m.m14 * 1,
							  m.m21 * v.X + m.m22 * v.Y + m.m23 * v.Z + m.m24 * 1,
							  m.m31 * v.X + m.m32 * v.Y + m.m33 * v.Z + m.m34 * 1);
		}

		public static Vector Transform(Matrix m, Vector v, float w = 1f) {
			return new Vector(m.m11 * v.X + m.m12 * v.Y + m.m13 * v.Z + m.m14 * w,
							  m.m21 * v.X + m.m22 * v.Y + m.m23 * v.Z + m.m24 * w,
							  m.m31 * v.X + m.m32 * v.Y + m.m33 * v.Z + m.m34 * w);
		}
		
		public static Matrix operator *(Matrix a, Matrix b) {
			return new Matrix(	
				b.m11*a.m11+b.m21*a.m12+b.m31*a.m13+b.m41*a.m14,
				b.m12*a.m11+b.m22*a.m12+b.m32*a.m13+b.m42*a.m14,
				b.m13*a.m11+b.m23*a.m12+b.m33*a.m13+b.m43*a.m14,
				b.m14*a.m11+b.m24*a.m12+b.m34*a.m13+b.m44*a.m14,

				b.m11*a.m21+b.m21*a.m22+b.m31*a.m23+b.m41*a.m24,
				b.m12*a.m21+b.m22*a.m22+b.m32*a.m23+b.m42*a.m24,
				b.m13*a.m21+b.m23*a.m22+b.m33*a.m23+b.m43*a.m24,
				b.m14*a.m21+b.m24*a.m22+b.m34*a.m23+b.m44*a.m24,

				b.m11*a.m31+b.m21*a.m32+b.m31*a.m33+b.m41*a.m34,
				b.m12*a.m31+b.m22*a.m32+b.m32*a.m33+b.m42*a.m34,
				b.m13*a.m31+b.m23*a.m32+b.m33*a.m33+b.m43*a.m34,
				b.m14*a.m31+b.m24*a.m32+b.m34*a.m33+b.m44*a.m34,

				b.m11*a.m41+b.m21*a.m42+b.m31*a.m43+b.m41*a.m44,
				b.m12*a.m41+b.m22*a.m42+b.m32*a.m43+b.m42*a.m44,
				b.m13*a.m41+b.m23*a.m42+b.m33*a.m43+b.m43*a.m44,
				b.m14*a.m41+b.m24*a.m42+b.m34*a.m43+b.m44*a.m44);
		}

		public static Matrix Translation(Vector translation) {
			var result = Identity;
			result.m14 = translation.X;
			result.m24 = translation.Y;
			result.m34 = translation.Z;
			return result;
		}

		public static Matrix Scale(Vector scale) {
			var result = Identity;
			result.m11 = scale.X;
			result.m22 = scale.Y;
			result.m33 = scale.Z;
			return result;
		}

		static Matrix RotationX(float x) {
			var result = Identity;
			result.m22 = MathF.Cos(x);
			result.m23 = -MathF.Sin(x);
			result.m32 = MathF.Sin(x);
			result.m33 = MathF.Cos(x);
			return result;
		}

		static Matrix RotationY(float y) {
			var result = Identity;
			result.m11 = MathF.Cos(y);
			result.m31 = -MathF.Sin(y);
			result.m13 = MathF.Sin(y);
			result.m33 = MathF.Cos(y);
			return result;
		}

		static Matrix RotationZ(float z) {
			var result = Identity;
			result.m11 = MathF.Cos(z);
			result.m12 = -MathF.Sin(z);
			result.m21 = MathF.Sin(z);
			result.m22 = MathF.Cos(z);
			return result;
		}

		public static Matrix Rotation(Vector rotation) {
			return RotationZ(rotation.Z) * RotationY(rotation.Y) * RotationX(rotation.X);
		}
		
		public static Matrix Perspective(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
		{
			if (fieldOfView <= 0.0f || fieldOfView >= Math.PI)
				throw new ArgumentOutOfRangeException("fieldOfView");

			if (nearPlaneDistance <= 0.0f)
				throw new ArgumentOutOfRangeException("nearPlaneDistance");

			if (farPlaneDistance <= 0.0f)
				throw new ArgumentOutOfRangeException("farPlaneDistance");

			if (nearPlaneDistance >= farPlaneDistance)
				throw new ArgumentOutOfRangeException("nearPlaneDistance");

			float tanHalfOff = (float)Math.Tan(fieldOfView * 0.5f);
			float zRange = farPlaneDistance - nearPlaneDistance;

			Matrix result;

			result.m11 = 1.0f/(aspectRatio*tanHalfOff);
			result.m12 = result.m13 = result.m14 = 0.0f;

			result.m22 = 1.0f/tanHalfOff;
			result.m21 = result.m23 = result.m24 = 0.0f;

			result.m31 = result.m32 = 0.0f;
			result.m33 = -(farPlaneDistance+nearPlaneDistance) / zRange;
			result.m34 = -1.0f;

			result.m41 = result.m42 = result.m44 = 0.0f;
			result.m43 = -2*nearPlaneDistance * farPlaneDistance / zRange;

			return result;
		}
		
		public Matrix Invert()
        {
            float a = m11, b = m21, c = m31, d = m41;
            float e = m12, f = m22, g = m32, h = m42;
            float i = m13, j = m23, k = m33, l = m43;
            float m = m14, n = m24, o = m34, p = m44;

            float kp_lo = k * p - l * o;
            float jp_ln = j * p - l * n;
            float jo_kn = j * o - k * n;
            float ip_lm = i * p - l * m;
            float io_km = i * o - k * m;
            float in_jm = i * n - j * m;

            float a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
            float a12 = -(e * kp_lo - g * ip_lm + h * io_km);
            float a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
            float a14 = -(e * jo_kn - f * io_km + g * in_jm);

            float det = a * a11 + b * a12 + c * a13 + d * a14;

            if (Math.Abs(det) < float.Epsilon)
            {
                return new Matrix(float.NaN, float.NaN, float.NaN, float.NaN,
                                       float.NaN, float.NaN, float.NaN, float.NaN,
                                       float.NaN, float.NaN, float.NaN, float.NaN,
                                       float.NaN, float.NaN, float.NaN, float.NaN);
            }

            float invDet = 1.0f / det;
            
            Matrix result;
            
            result.m11 = a11 * invDet;
            result.m12 = a12 * invDet;
            result.m13 = a13 * invDet;
            result.m14 = a14 * invDet;

            result.m21 = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
            result.m22 = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
            result.m23 = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
            result.m24 = +(a * jo_kn - b * io_km + c * in_jm) * invDet;

            float gp_ho = g * p - h * o;
            float fp_hn = f * p - h * n;
            float fo_gn = f * o - g * n;
            float ep_hm = e * p - h * m;
            float eo_gm = e * o - g * m;
            float en_fm = e * n - f * m;

            result.m31 = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
            result.m32 = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
            result.m33 = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
            result.m34 = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

            float gl_hk = g * l - h * k;
            float fl_hj = f * l - h * j;
            float fk_gj = f * k - g * j;
            float el_hi = e * l - h * i;
            float ek_gi = e * k - g * i;
            float ej_fi = e * j - f * i;

            result.m41 = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
            result.m42 = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
            result.m43 = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
            result.m44 = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;

            return result;
        }
		public static Matrix FromBaseVectors(Vector xAxis, Vector yAxis, Vector zAxis)
		{
			Matrix result = Identity;
			result.m11 = xAxis.X;
			result.m21 = xAxis.Y;
			result.m31 = xAxis.Z;
    
			result.m12 = yAxis.X;
			result.m22 = yAxis.Y;
			result.m32 = yAxis.Z;
    
			result.m13 = zAxis.X;
			result.m23 = zAxis.Y;
			result.m33 = zAxis.Z;

			return result;
		}
	}
}