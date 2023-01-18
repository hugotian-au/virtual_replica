cd Assets/Scenes
if exist room.unity del room.unity 
if "%1"=="0" copy room0_pyramid_condition0_annotation.unity room.unity
if "%1"=="1" copy room1_pyramid_condition1_segment.unity room.unity
if "%1"=="2" copy room2_pyramid_condition2_annotation_segment.unity room.unity
if "%1"=="3" copy room3_bath_condition0_annotation.unity room.unity
if "%1"=="4" copy room4_crystal_condition1_segment.unity room.unity
if "%1"=="5" copy room5_cube_condition2_annotation_segment.unity room.unity
if "%1"=="6" copy room6_sofa_condition0_annotation.unity room.unity
if "%1"=="7" copy room7_terrace_condition1_segment.unity room.unity
if "%1"=="8" copy room8_tunnel_condition2_annotation_segment.unity room.unity

cd ../..
