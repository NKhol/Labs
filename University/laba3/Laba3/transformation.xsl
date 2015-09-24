<?xml version='1.0'?>
<xsl:stylesheet version="1.0"
      xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method= "html"/>
  <xsl:template match="department">
    <h1><xsl:value-of select="@NANE"/></h1>
    <TABLE BORDER ="5">
      <TR>
    <xsl:for-each select="employee">
      <TR>
        <TD>
      <TABLE BORDER ="1" WIDTH ="1200">
        <TR>
        <th style="width:10%;">
          <p align ="left">ПІП</p>
        </th>
        <th style="width:90%;">
          <p align ="left">
            <xsl:value-of select="@NAME"/>
          </p>
        </th>
      </TR>
        <TR>
        <th style="width:10%;">
          <p align ="left">Посада</p>
        </th>
        <th style="width:90%;">
          <p align ="left">
            <xsl:value-of select="@POSITION"/>
          </p>
        </th>
      </TR>
        <TR>
          <th style="width:10%;">
            <p align ="left">Освіта</p>
          </th>
          <th style="width:90%;">
            <p align ="left">
              <xsl:value-of select="@DEGREE"/>
            </p>
          </th>
        </TR>

        <TR>
          <th style="width:10%;">
            <p align ="left">Звання</p>
          </th>
          <th style="width:90%;">
            <p align ="left">
              <xsl:for-each select ="rank">
                <xsl:value-of select="@RANK"/>;
              </xsl:for-each>
            </p>
          </th>
        </TR>
        
        <TR>
          <th style="width:10%;">
            <p align ="left">Аудиторія</p>
          </th>
          <th style="width:90%;">
            <p align ="left">
              <xsl:value-of select="@AUDIENCE"/>
            </p>
          </th>
        </TR>
        <TR>
          <th style="width:10%;">
            <p align ="left">Телефон</p>
          </th>
          <th style="width:90%;">
            <p align ="left">
              <xsl:value-of select="@PHONE"/>
            </p>
          </th>
        </TR>
        <TR>
          <th style="width:10%;">
            <p align ="left">Інтереси</p>
          </th>
          <th style="width:90%;">
            <p align ="left">
              <xsl:value-of select="@INTERESTS"/>
            </p>
          </th>
        </TR>
      </TABLE>
        </TD>
      </TR>
    </xsl:for-each>
      </TR>
    </TABLE>
  </xsl:template>
  
  
</xsl:stylesheet>