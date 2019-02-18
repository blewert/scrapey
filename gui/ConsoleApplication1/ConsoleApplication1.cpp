#include "stdafx.h"

#if _MSC_VER 
	#define EXPORT_API __declspec(dllexport) 
#else
	#define EXPORT_API 
#endif

// Link following functions C-style (required for plugins)
extern "C"
{
	// The functions we will call from Unity.
	//
	const EXPORT_API char*  PrintHello() {
		return "Hello";
	}

	int EXPORT_API PrintANumber() {
		return 5;
	}

	int EXPORT_API AddTwoIntegers(int a, int b) {
		return a + b;
	}

	float EXPORT_API AddTwoFloats(float a, float b) {
		return a + b;
	}

} // end of export C block
