;#DropDB AirTransSolutionEasyTest

#Application AirTransSolution.Win
#Application AirTransSolution.Web
#IfDef AirTransSolution.Win

*Action About...
 
*Action OK

#EndIf

#IfDef AirTransSolution.Web
!Action Navigation(My Details)
#EndIf
