�
�D:\Banobras\Sistemas .NET\SST3\SIVARMER\TransicionNvaFSW\Trunk\Código\API_DERIVADOS_ETAPA4_2024_06_06\apiRiesgos\DAL\DALRiesgos.cs
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
conexionRiesgos	xxr �
)
xx� �
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
�� 
	Respuesta
�� "
guardarGlobalTitulos
�� -
(
��- .
List
��. 2
<
��2 3#
PosicionGlobalTitulos
��3 H
>
��H I
reporte
��J Q
,
��Q R
OracleConnection
��S c
con
��d g
)
��g h
{
�� 	
	Respuesta
�� 
resp
�� 
=
�� 
new
��  

DAORiesgos
��! +
(
��+ ,
)
��, -
.
��- .*
guardarPosicionGlobalTitulos
��. J
(
��J K
reporte
��K R
,
��R S
con
��T W
)
��W X
;
��X Y
return
�� 
resp
�� 
;
�� 
}
�� 	
public
�� 
	Respuesta
�� )
guardarMovimientosTesoreria
�� 4
(
��4 5
List
��5 9
<
��9 :"
MovimientosTesoreria
��: N
>
��N O
reporte
��P W
,
��W X
OracleConnection
��Y i
conexionRiesgos
��j y
)
��y z
{
�� 	
	Respuesta
�� 
resp
�� 
=
�� 
new
��  

DAORiesgos
��! +
(
��+ ,
)
��, -
.
��- .)
guardarMovimientosTesoreria
��. I
(
��I J
reporte
��J Q
,
��Q R
conexionRiesgos
��S b
)
��b c
;
��c d
return
�� 
resp
�� 
;
�� 
}
�� 	
public
�� 
	Respuesta
�� %
guardarPosicionForwards
�� 0
(
��0 1
List
��1 5
<
��5 6
PosicionForwards
��6 F
>
��F G
reporte
��H O
,
��O P
OracleConnection
��Q a
conexionRiesgos
��b q
)
��q r
{
�� 	
	Respuesta
�� 
resp
�� 
=
�� 
new
��  

DAORiesgos
��! +
(
��+ ,
)
��, -
.
��- .%
guardarPosicionForwards
��. E
(
��E F
reporte
��F M
,
��M N
conexionRiesgos
��O ^
)
��^ _
;
��_ `
return
�� 
resp
�� 
;
�� 
}
�� 	
public
�� 
	Respuesta
��  
guardarFlujosSwaps
�� +
(
��+ ,
List
��, 0
<
��0 1
FlujosSwaps
��1 <
>
��< =
reporte
��> E
,
��E F
List
��G K
<
��K L
CatalogoDivisas
��L [
>
��[ \
catalogoDivisas
��] l
,
��l m
OracleConnection
��n ~
con�� �
)��� �
{
�� 	
	Respuesta
�� 
resp
�� 
=
�� 
new
��  

DAORiesgos
��! +
(
��+ ,
)
��, -
.
��- . 
guardarFlujosSwaps
��. @
(
��@ A
reporte
��A H
,
��H I
catalogoDivisas
��J Y
,
��Y Z
con
��[ ^
)
��^ _
;
��_ `
return
�� 
resp
�� 
;
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 
	Respuesta
�� #
>
��# $.
 FlujosPosicionesPrimariasProcess
��% E
(
��E F
int
��F I
fechaConsulta
��J W
,
��W X
List
��Y ]
<
��] ^'
FlujosPosicionesPrimarias
��^ w
>
��w x
reporte��y �
,��� � 
OracleConnection��� �
con��� �
)��� �
{
�� 	
	Respuesta
�� 
resp
�� 
=
�� 
await
�� "
new
��# &

DAORiesgos
��' 1
(
��1 2
)
��2 3
.
��3 4.
 FlujosPosicionesPrimariasProcess
��4 T
(
��T U
fechaConsulta
��U b
,
��b c
reporte
��d k
,
��k l
con
��m p
)
��p q
;
��q r
return
�� 
resp
�� 
;
�� 
}
�� 	
public
�� 
	Respuesta
�� )
guardarCaracteristicasSwaps
�� 4
(
��4 5
List
��5 9
<
��9 :"
CaracteristicasSwaps
��: N
>
��N O
reporte
��P W
,
��W X
List
��Y ]
<
��] ^
CatalogoDivisas
��^ m
>
��m n
catalogoDivisas
��o ~
,
��~  
OracleConnection��� �
con��� �
)��� �
{
�� 	
	Respuesta
�� 
resp
�� 
=
�� 
new
��  

DAORiesgos
��! +
(
��+ ,
)
��, -
.
��- .)
guardarCaracteristicasSwaps
��. I
(
��I J
reporte
��J Q
,
��Q R
catalogoDivisas
��S b
,
��b c
con
��d g
)
��g h
;
��h i
return
�� 
resp
�� 
;
�� 
}
�� 	
public
�� 
	Respuesta
�� "
guardarLlamadaMargen
�� -
(
��- .
List
��. 2
<
��2 3
LlamadaMargen
��3 @
>
��@ A
reporte
��B I
,
��I J
OracleConnection
��K [
con
��\ _
)
��_ `
{
�� 	
	Respuesta
�� 
resp
�� 
=
�� 
new
��  

DAORiesgos
��! +
(
��+ ,
)
��, -
.
��- ."
guardarLlamadaMargen
��. B
(
��B C
reporte
��C J
,
��J K
con
��L O
)
��O P
;
��P Q
return
�� 
resp
�� 
;
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 
	Respuesta
�� #
>
��# $*
guardarPosicionPrimariaSwaps
��% A
(
��A B
int
��B E
fechaConsulta
��F S
,
��S T
List
��U Y
<
��Y Z#
PosicionPrimariaSwaps
��Z o
>
��o p
reporte
��q x
,
��x y
OracleConnection��z �
con��� �
)��� �
{
�� 	
	Respuesta
�� 
resp
�� 
=
�� 
await
�� "
new
��# &

DAORiesgos
��' 1
(
��1 2
)
��2 3
.
��3 4*
guardarPosicionPrimariaSwaps
��4 P
(
��P Q
fechaConsulta
��Q ^
,
��^ _
reporte
��` g
,
��g h
con
��i l
)
��l m
;
��m n
return
�� 
resp
�� 
;
�� 
}
�� 	
public
�� 
	Respuesta
�� .
 guardarReporteOperacionCVDivisas
�� 9
(
��9 :
List
��: >
<
��> ?'
ReporteOperacionCVDivisas
��? X
>
��X Y
reporte
��Z a
,
��a b
List
��c g
<
��g h
CatalogoDivisas
��h w
>
��w x
catalogoDivisas��y �
,��� �
List��� �
<��� �"
CatalogoPosiciones��� �
>��� �"
catalogoPosiciones��� �
,��� � 
OracleConnection��� �
conexionRiesgos��� �
)��� �
{
�� 	
	Respuesta
�� 
resp
�� 
=
�� 
new
��  

DAORiesgos
��! +
(
��+ ,
)
��, -
.
��- ..
 guardarReporteOperacionCVDivisas
��. N
(
��N O
reporte
��O V
,
��V W
catalogoDivisas
��X g
,
��g h 
catalogoPosiciones
��i {
,
��{ |
conexionRiesgos��} �
)��� �
;��� �
return
�� 
resp
�� 
;
�� 
}
�� 	
public
�� 
	Respuesta
�� +
guardarPosicionesPrimForwards
�� 6
(
��6 7
List
��7 ;
<
��; <$
PosicionesPrimForwards
��< R
>
��R S
reporte
��T [
,
��[ \
List
��] a
<
��a b
CatalogoDivisas
��b q
>
��q r
catalogoDivisas��s �
,��� � 
OracleConnection��� �
conexionRiesgos��� �
)��� �
{
�� 	
	Respuesta
�� 
resp
�� 
=
�� 
new
��  

DAORiesgos
��! +
(
��+ ,
)
��, -
.
��- .+
guardarPosicionesPrimForwards
��. K
(
��K L
reporte
��L S
,
��S T
catalogoDivisas
��U d
,
��d e
conexionRiesgos
��f u
)
��u v
;
��v w
return
�� 
resp
�� 
;
�� 
}
�� 	
public
�� 
	Respuesta
�� *
guardarComprasVentasOperador
�� 5
(
��5 6
List
��6 :
<
��: ;#
ComprasVentasOperador
��; P
>
��P Q
reporte
��R Y
,
��Y Z
OracleConnection
��[ k
con
��l o
,
��o p
int
��q t
fechaIni
��u }
,
��} ~
int�� �
fechaFin��� �
)��� �
{
�� 	
	Respuesta
�� 
resp
�� 
=
�� 
new
��  

DAORiesgos
��! +
(
��+ ,
)
��, -
.
��- .*
guardarComprasVentasOperador
��. J
(
��J K
reporte
��K R
,
��R S
con
��T W
,
��W X
fechaIni
��Y a
,
��a b
fechaFin
��c k
)
��k l
;
��l m
return
�� 
resp
�� 
;
�� 
}
�� 	
public
�� 
List
�� 
<
�� 
CatalogoDivisas
�� #
>
��# $ 
GetCatalogoDivisas
��% 7
(
��7 8
string
��8 >
esquema
��? F
,
��F G
OracleConnection
��H X
conexionRiesgos
��Y h
)
��h i
{
�� 	
var
�� 
response
�� 
=
�� 
new
�� 

DAORiesgos
�� )
(
��) *
)
��* +
.
��+ , 
GetCatalogoDivisas
��, >
(
��> ?
esquema
��? F
,
��F G
conexionRiesgos
��H W
)
��W X
;
��X Y
return
�� 
response
�� 
;
�� 
}
�� 	
public
�� 
List
�� 
<
��  
CatalogoPosiciones
�� &
>
��& '#
GetCatalogoPosiciones
��( =
(
��= >
OracleConnection
��> N
conexionRiesgos
��O ^
)
��^ _
{
�� 	
var
�� 
response
�� 
=
�� 
new
�� 

DAORiesgos
�� )
(
��) *
)
��* +
.
��+ ,#
GetCatalogoPosiciones
��, A
(
��A B
conexionRiesgos
��B Q
)
��Q R
;
��R S
return
�� 
response
�� 
;
�� 
}
�� 	
}
�� 
}�� 