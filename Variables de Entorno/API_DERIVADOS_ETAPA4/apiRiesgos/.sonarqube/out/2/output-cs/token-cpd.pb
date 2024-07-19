ÙÉ
ÉD:\Banobras\Sistemas .NET\SST3\SIVARMER\TransicionNvaFSW\Trunk\C√≥digo\API_DERIVADOS_ETAPA4_2024_06_06\apiRiesgos\DAL\DALRiesgos.cs
	namespace 	
DAL
 
{ 
public 

class 

DALRiesgos 
{ 
public 
	Respuesta $
guardarValuacionReportos 1
(1 2
List2 6
<6 7
ValuacionReportos7 H
>H I
reporteJ Q
,Q R
OracleConnectionS c
cond g
)g h
{ 	
	Respuesta 
resp 
= 
new  

DAORiesgos! +
(+ ,
), -
.- .$
guardarValuacionReportos. F
(F G
reporteG N
,N O
conP S
)S T
;T U
return 
resp 
; 
} 	
public 
	Respuesta "
guardarTenenciaTitulos /
(/ 0
List0 4
<4 5
TenenciaTitulos5 D
>D E
reporteF M
,M N
OracleConnectionO _
con` c
)c d
{ 	
	Respuesta 
resp 
= 
new  

DAORiesgos! +
(+ ,
), -
.- ."
guardarTenenciaTitulos. D
(D E
reporteE L
,L M
conN Q
)Q R
;R S
return!! 
resp!! 
;!! 
}"" 	
public** 
	Respuesta** $
guardarComprasMesaDinero** 1
(**1 2
List**2 6
<**6 7
ComprasMesaDinero**7 H
>**H I
reporte**J Q
,**Q R
OracleConnection**S c
con**d g
)**g h
{++ 	
	Respuesta,, 
resp,, 
=,, 
new,,  

DAORiesgos,,! +
(,,+ ,
),,, -
.,,- .$
guardarComprasMesaDinero,,. F
(,,F G
reporte,,G N
,,,N O
con,,P S
),,S T
;,,T U
return.. 
resp.. 
;.. 
}// 	
public77 
	Respuesta77 #
guardarComprasTesoreria77 0
(770 1
List771 5
<775 6
ComprasTesoreria776 F
>77F G
reporte77H O
,77O P
OracleConnection77Q a
con77b e
)77e f
{88 	
	Respuesta99 
resp99 
=99 
new99  

DAORiesgos99! +
(99+ ,
)99, -
.99- .#
guardarComprasTesoreria99. E
(99E F
reporte99F M
,99M N
con99O R
)99R S
;99S T
return;; 
resp;; 
;;; 
}<< 	
publicDD 
	RespuestaDD &
guardarPosicionPatrimonialDD 3
(DD3 4
ListDD4 8
<DD8 9
PosicionPatrimonialDD9 L
>DDL M
reporteDDN U
,DDU V
OracleConnectionDDW g
conDDh k
)DDk l
{EE 	
	RespuestaFF 
respFF 
=FF 
newFF  

DAORiesgosFF! +
(FF+ ,
)FF, -
.FF- .&
guardarPosicionPatrimonialFF. H
(FFH I
reporteFFI P
,FFP Q
conFFR U
)FFU V
;FFV W
returnHH 
respHH 
;HH 
}II 	
publicQQ 
	RespuestaQQ  
guardarReporteREVAMEQQ -
(QQ- .
ListQQ. 2
<QQ2 3
ReporteRevameQQ3 @
>QQ@ A
reporteQQB I
,QQI J
OracleConnectionQQK [
conQQ\ _
)QQ_ `
{RR 	
	RespuestaSS 
respSS 
=SS 
newSS  

DAORiesgosSS! +
(SS+ ,
)SS, -
.SS- . 
guardarReporteREVAMESS. B
(SSB C
reporteSSC J
,SSJ K
conSSL O
)SSO P
;SSP Q
returnUU 
respUU 
;UU 
}VV 	
public^^ 
	Respuesta^^ %
guardarPosicionCalculoVAR^^ 2
(^^2 3
List^^3 7
<^^7 8
PosicionCalculoVar^^8 J
>^^J K
reporte^^L S
,^^S T
OracleConnection^^U e
conexionRiesgos^^f u
)^^u v
{__ 	
	Respuesta`` 
resp`` 
=`` 
new``  

DAORiesgos``! +
(``+ ,
)``, -
.``- .%
guardarPosicionCalculoVAR``. G
(``G H
reporte``H O
,``O P
conexionRiesgos``Q `
)``` a
;``a b
returnbb 
respbb 
;bb 
}cc 	
publickk 
	Respuestakk '
guardarPosicionRegulatorioskk 4
(kk4 5
Listkk5 9
<kk9 : 
PosicionRegulatorioskk: N
>kkN O
reportekkP W
,kkW X
OracleConnectionkkY i
conexionRiesgoskkj y
)kky z
{ll 	
	Respuestamm 
respmm 
=mm 
newmm  

DAORiesgosmm! +
(mm+ ,
)mm, -
.mm- .'
guardarPosicionRegulatoriosmm. I
(mmI J
reportemmJ Q
,mmQ R
conexionRiesgosmmS b
)mmb c
;mmc d
returnoo 
respoo 
;oo 
}pp 	
publicxx 
	Respuestaxx +
guardarReportePosicionTesoreriaxx 8
(xx8 9
Listxx9 =
<xx= >$
ReportePosicionTesoreriaxx> V
>xxV W
reportexxX _
,xx_ `
OracleConnectionxxa q
conexionRiesgos	xxr Å
)
xxÅ Ç
{yy 	
	Respuestazz 
respzz 
=zz 
newzz  

DAORiesgoszz! +
(zz+ ,
)zz, -
.zz- .+
guardarReportePosicionTesoreriazz. M
(zzM N
reportezzN U
,zzU V
conexionRiesgoszzW f
)zzf g
;zzg h
return|| 
resp|| 
;|| 
}}} 	
public
ÖÖ 
	Respuesta
ÖÖ "
guardarGlobalTitulos
ÖÖ -
(
ÖÖ- .
List
ÖÖ. 2
<
ÖÖ2 3#
PosicionGlobalTitulos
ÖÖ3 H
>
ÖÖH I
reporte
ÖÖJ Q
,
ÖÖQ R
OracleConnection
ÖÖS c
con
ÖÖd g
)
ÖÖg h
{
ÜÜ 	
	Respuesta
áá 
resp
áá 
=
áá 
new
áá  

DAORiesgos
áá! +
(
áá+ ,
)
áá, -
.
áá- .*
guardarPosicionGlobalTitulos
áá. J
(
ááJ K
reporte
ááK R
,
ááR S
con
ááT W
)
ááW X
;
ááX Y
return
ââ 
resp
ââ 
;
ââ 
}
ää 	
public
íí 
	Respuesta
íí )
guardarMovimientosTesoreria
íí 4
(
íí4 5
List
íí5 9
<
íí9 :"
MovimientosTesoreria
íí: N
>
ííN O
reporte
ííP W
,
ííW X
OracleConnection
ííY i
conexionRiesgos
ííj y
)
ííy z
{
ìì 	
	Respuesta
îî 
resp
îî 
=
îî 
new
îî  

DAORiesgos
îî! +
(
îî+ ,
)
îî, -
.
îî- .)
guardarMovimientosTesoreria
îî. I
(
îîI J
reporte
îîJ Q
,
îîQ R
conexionRiesgos
îîS b
)
îîb c
;
îîc d
return
ññ 
resp
ññ 
;
ññ 
}
óó 	
public
üü 
	Respuesta
üü %
guardarPosicionForwards
üü 0
(
üü0 1
List
üü1 5
<
üü5 6
PosicionForwards
üü6 F
>
üüF G
reporte
üüH O
,
üüO P
OracleConnection
üüQ a
conexionRiesgos
üüb q
)
üüq r
{
†† 	
	Respuesta
°° 
resp
°° 
=
°° 
new
°°  

DAORiesgos
°°! +
(
°°+ ,
)
°°, -
.
°°- .%
guardarPosicionForwards
°°. E
(
°°E F
reporte
°°F M
,
°°M N
conexionRiesgos
°°O ^
)
°°^ _
;
°°_ `
return
££ 
resp
££ 
;
££ 
}
§§ 	
public
¨¨ 
	Respuesta
¨¨  
guardarFlujosSwaps
¨¨ +
(
¨¨+ ,
List
¨¨, 0
<
¨¨0 1
FlujosSwaps
¨¨1 <
>
¨¨< =
reporte
¨¨> E
,
¨¨E F
List
¨¨G K
<
¨¨K L
CatalogoDivisas
¨¨L [
>
¨¨[ \
catalogoDivisas
¨¨] l
,
¨¨l m
OracleConnection
¨¨n ~
con¨¨ Ç
)¨¨Ç É
{
≠≠ 	
	Respuesta
ÆÆ 
resp
ÆÆ 
=
ÆÆ 
new
ÆÆ  

DAORiesgos
ÆÆ! +
(
ÆÆ+ ,
)
ÆÆ, -
.
ÆÆ- . 
guardarFlujosSwaps
ÆÆ. @
(
ÆÆ@ A
reporte
ÆÆA H
,
ÆÆH I
catalogoDivisas
ÆÆJ Y
,
ÆÆY Z
con
ÆÆ[ ^
)
ÆÆ^ _
;
ÆÆ_ `
return
∞∞ 
resp
∞∞ 
;
∞∞ 
}
±± 	
public
ππ 
async
ππ 
Task
ππ 
<
ππ 
	Respuesta
ππ #
>
ππ# $.
 FlujosPosicionesPrimariasProcess
ππ% E
(
ππE F
int
ππF I
fechaConsulta
ππJ W
,
ππW X
List
ππY ]
<
ππ] ^'
FlujosPosicionesPrimarias
ππ^ w
>
ππw x
reporteππy Ä
,ππÄ Å 
OracleConnectionππÇ í
conππì ñ
)ππñ ó
{
∫∫ 	
	Respuesta
ªª 
resp
ªª 
=
ªª 
await
ªª "
new
ªª# &

DAORiesgos
ªª' 1
(
ªª1 2
)
ªª2 3
.
ªª3 4.
 FlujosPosicionesPrimariasProcess
ªª4 T
(
ªªT U
fechaConsulta
ªªU b
,
ªªb c
reporte
ªªd k
,
ªªk l
con
ªªm p
)
ªªp q
;
ªªq r
return
ΩΩ 
resp
ΩΩ 
;
ΩΩ 
}
ææ 	
public
∆∆ 
	Respuesta
∆∆ )
guardarCaracteristicasSwaps
∆∆ 4
(
∆∆4 5
List
∆∆5 9
<
∆∆9 :"
CaracteristicasSwaps
∆∆: N
>
∆∆N O
reporte
∆∆P W
,
∆∆W X
List
∆∆Y ]
<
∆∆] ^
CatalogoDivisas
∆∆^ m
>
∆∆m n
catalogoDivisas
∆∆o ~
,
∆∆~  
OracleConnection∆∆Ä ê
con∆∆ë î
)∆∆î ï
{
«« 	
	Respuesta
»» 
resp
»» 
=
»» 
new
»»  

DAORiesgos
»»! +
(
»»+ ,
)
»», -
.
»»- .)
guardarCaracteristicasSwaps
»». I
(
»»I J
reporte
»»J Q
,
»»Q R
catalogoDivisas
»»S b
,
»»b c
con
»»d g
)
»»g h
;
»»h i
return
   
resp
   
;
   
}
ÀÀ 	
public
”” 
	Respuesta
”” "
guardarLlamadaMargen
”” -
(
””- .
List
””. 2
<
””2 3
LlamadaMargen
””3 @
>
””@ A
reporte
””B I
,
””I J
OracleConnection
””K [
con
””\ _
)
””_ `
{
‘‘ 	
	Respuesta
’’ 
resp
’’ 
=
’’ 
new
’’  

DAORiesgos
’’! +
(
’’+ ,
)
’’, -
.
’’- ."
guardarLlamadaMargen
’’. B
(
’’B C
reporte
’’C J
,
’’J K
con
’’L O
)
’’O P
;
’’P Q
return
◊◊ 
resp
◊◊ 
;
◊◊ 
}
ÿÿ 	
public
‡‡ 
async
‡‡ 
Task
‡‡ 
<
‡‡ 
	Respuesta
‡‡ #
>
‡‡# $*
guardarPosicionPrimariaSwaps
‡‡% A
(
‡‡A B
int
‡‡B E
fechaConsulta
‡‡F S
,
‡‡S T
List
‡‡U Y
<
‡‡Y Z#
PosicionPrimariaSwaps
‡‡Z o
>
‡‡o p
reporte
‡‡q x
,
‡‡x y
OracleConnection‡‡z ä
con‡‡ã é
)‡‡é è
{
·· 	
	Respuesta
‚‚ 
resp
‚‚ 
=
‚‚ 
await
‚‚ "
new
‚‚# &

DAORiesgos
‚‚' 1
(
‚‚1 2
)
‚‚2 3
.
‚‚3 4*
guardarPosicionPrimariaSwaps
‚‚4 P
(
‚‚P Q
fechaConsulta
‚‚Q ^
,
‚‚^ _
reporte
‚‚` g
,
‚‚g h
con
‚‚i l
)
‚‚l m
;
‚‚m n
return
‰‰ 
resp
‰‰ 
;
‰‰ 
}
ÂÂ 	
public
ÌÌ 
	Respuesta
ÌÌ .
 guardarReporteOperacionCVDivisas
ÌÌ 9
(
ÌÌ9 :
List
ÌÌ: >
<
ÌÌ> ?'
ReporteOperacionCVDivisas
ÌÌ? X
>
ÌÌX Y
reporte
ÌÌZ a
,
ÌÌa b
List
ÌÌc g
<
ÌÌg h
CatalogoDivisas
ÌÌh w
>
ÌÌw x
catalogoDivisasÌÌy à
,ÌÌà â
ListÌÌä é
<ÌÌé è"
CatalogoPosicionesÌÌè °
>ÌÌ° ¢"
catalogoPosicionesÌÌ£ µ
,ÌÌµ ∂ 
OracleConnectionÌÌ∑ «
conexionRiesgosÌÌ» ◊
)ÌÌ◊ ÿ
{
ÓÓ 	
	Respuesta
ÔÔ 
resp
ÔÔ 
=
ÔÔ 
new
ÔÔ  

DAORiesgos
ÔÔ! +
(
ÔÔ+ ,
)
ÔÔ, -
.
ÔÔ- ..
 guardarReporteOperacionCVDivisas
ÔÔ. N
(
ÔÔN O
reporte
ÔÔO V
,
ÔÔV W
catalogoDivisas
ÔÔX g
,
ÔÔg h 
catalogoPosiciones
ÔÔi {
,
ÔÔ{ |
conexionRiesgosÔÔ} å
)ÔÔå ç
;ÔÔç é
return
ÒÒ 
resp
ÒÒ 
;
ÒÒ 
}
ÚÚ 	
public
˙˙ 
	Respuesta
˙˙ +
guardarPosicionesPrimForwards
˙˙ 6
(
˙˙6 7
List
˙˙7 ;
<
˙˙; <$
PosicionesPrimForwards
˙˙< R
>
˙˙R S
reporte
˙˙T [
,
˙˙[ \
List
˙˙] a
<
˙˙a b
CatalogoDivisas
˙˙b q
>
˙˙q r
catalogoDivisas˙˙s Ç
,˙˙Ç É 
OracleConnection˙˙Ñ î
conexionRiesgos˙˙ï §
)˙˙§ •
{
˚˚ 	
	Respuesta
¸¸ 
resp
¸¸ 
=
¸¸ 
new
¸¸  

DAORiesgos
¸¸! +
(
¸¸+ ,
)
¸¸, -
.
¸¸- .+
guardarPosicionesPrimForwards
¸¸. K
(
¸¸K L
reporte
¸¸L S
,
¸¸S T
catalogoDivisas
¸¸U d
,
¸¸d e
conexionRiesgos
¸¸f u
)
¸¸u v
;
¸¸v w
return
˛˛ 
resp
˛˛ 
;
˛˛ 
}
ˇˇ 	
public
ââ 
	Respuesta
ââ *
guardarComprasVentasOperador
ââ 5
(
ââ5 6
List
ââ6 :
<
ââ: ;#
ComprasVentasOperador
ââ; P
>
ââP Q
reporte
ââR Y
,
ââY Z
OracleConnection
ââ[ k
con
ââl o
,
ââo p
int
ââq t
fechaIni
ââu }
,
ââ} ~
intââ Ç
fechaFinââÉ ã
)ââã å
{
ää 	
	Respuesta
ãã 
resp
ãã 
=
ãã 
new
ãã  

DAORiesgos
ãã! +
(
ãã+ ,
)
ãã, -
.
ãã- .*
guardarComprasVentasOperador
ãã. J
(
ããJ K
reporte
ããK R
,
ããR S
con
ããT W
,
ããW X
fechaIni
ããY a
,
ããa b
fechaFin
ããc k
)
ããk l
;
ããl m
return
çç 
resp
çç 
;
çç 
}
éé 	
public
êê 
List
êê 
<
êê 
CatalogoDivisas
êê #
>
êê# $ 
GetCatalogoDivisas
êê% 7
(
êê7 8
string
êê8 >
esquema
êê? F
,
êêF G
OracleConnection
êêH X
conexionRiesgos
êêY h
)
êêh i
{
ëë 	
var
íí 
response
íí 
=
íí 
new
íí 

DAORiesgos
íí )
(
íí) *
)
íí* +
.
íí+ , 
GetCatalogoDivisas
íí, >
(
íí> ?
esquema
íí? F
,
ííF G
conexionRiesgos
ííH W
)
ííW X
;
ííX Y
return
ìì 
response
ìì 
;
ìì 
}
îî 	
public
ññ 
List
ññ 
<
ññ  
CatalogoPosiciones
ññ &
>
ññ& '#
GetCatalogoPosiciones
ññ( =
(
ññ= >
OracleConnection
ññ> N
conexionRiesgos
ññO ^
)
ññ^ _
{
óó 	
var
òò 
response
òò 
=
òò 
new
òò 

DAORiesgos
òò )
(
òò) *
)
òò* +
.
òò+ ,#
GetCatalogoPosiciones
òò, A
(
òòA B
conexionRiesgos
òòB Q
)
òòQ R
;
òòR S
return
ôô 
response
ôô 
;
ôô 
}
öö 	
}
úú 
}ùù 