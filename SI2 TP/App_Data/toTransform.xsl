<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:template match="/">
    <html>
      <head><title>Clientes</title></head>
      <body>
        <h1>Clientes</h1>
        <xsl:for-each select="Clientes/Empresa">
          <h2>Empresa</h2>
          <h3><xsl:value-of select="NIPC"/></h3>
          <h3><xsl:value-of select="design"/></h3>
          <h3><xsl:value-of select="morada"/></h3>
          <table border="1">
            <tr bgcolor="#9acd32">
              <th>Codigo</th>
              <th>Descricao</th>
              <th>Morada</th>
              <th>Geolocalizacao</th>
            </tr>
            <xsl:for-each select="Instalacao">
              <tr>
                <td>
                  <xsl:value-of select="cod" />
                </td>
                <td>
                  <xsl:value-of select="descr" />
                </td>
                <td>
                  <xsl:value-of select="morada" />
                </td>
                <td>
                  <xsl:value-of select="geo" />
                </td>
              </tr>
            </xsl:for-each>
          </table>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>