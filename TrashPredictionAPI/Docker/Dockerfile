FROM python:3.10-slim

# Set workdir
WORKDIR /app

# Copy requirements and install only needed packages
COPY requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

# Copy your app and model files
COPY . .

# Expose port and run uvicorn
EXPOSE 8000
CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8000"]