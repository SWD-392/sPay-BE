-- Bảng ADMIN
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'ADMIN') 
  THEN 
    CREATE TABLE ADMIN (
      ADMIN_KEY SERIAL PRIMARY KEY,
      USER_KEY INT,
      ADMIN_NAME VARCHAR(50)
    );
  END IF; 
END $$;

-- Bảng CARD
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'CARD') 
  THEN 
    CREATE TABLE CARD (
      CARD_KEY SERIAL PRIMARY KEY,
      CUSTOMER_KEY INT,
      CARD_TYPE_KEY INT,
      CARD_NUMBER TEXT,
      CREATED_AT TIMESTAMP,
      CREATE_DATE TIMESTAMP,
      EXPIRY_DATE TIMESTAMP,
      STATUS TEXT
    );
  END IF; 
END $$;

-- Bảng CARD_STORE_CATEGORY
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'CARD_STORE_CATEGORY') 
  THEN 
    CREATE TABLE CARD_STORE_CATEGORY (
      KEY SERIAL PRIMARY KEY,
      CARD_TYPE_KEY INT,
      STORE_CATEGORY_KEY INT
    );
  END IF; 
END $$;

-- Bảng CARD_TYPE
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'CARD_TYPE') 
  THEN 
    CREATE TABLE CARD_TYPE (
      CARD_TYPE_KEY SERIAL PRIMARY KEY,
      NAME VARCHAR(255),
      DESCRIPTION VARCHAR(255)
    );
  END IF; 
END $$;

-- Bảng CUSTOMER
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'CUSTOMER') 
  THEN 
    CREATE TABLE CUSTOMER (
      CUSTOMER_KEY SERIAL PRIMARY KEY,
      USER_KEY INT,
      FIRST_NAME VARCHAR(50),
      LAST_NAME VARCHAR(50),
      EMAIL VARCHAR(100),
      ADDRESS VARCHAR(255),
      UNIQUE (USER_KEY)
    );
  END IF; 
END $$;

-- Bảng ORDER
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'ORDER') 
  THEN 
    CREATE TABLE "ORDER" (
      ORDER_KEY SERIAL PRIMARY KEY,
      STORE_KEY INT,
      CARD_KEY INT,
      AMOUNT INT,
      DATE DATE,
      STATUS BOOLEAN
    );
  END IF; 
END $$;

-- Bảng STORE
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'STORE') 
  THEN 
    CREATE TABLE STORE (
      STORE_KEY SERIAL PRIMARY KEY,
      NAME TEXT,
      CATEGORY_KEY INT,
      LOCATION TEXT,
      PHONE INT,
      STATUS BOOLEAN
    );
  END IF; 
END $$;

-- Bảng STORE_CATEGORY
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'STORE_CATEGORY') 
  THEN 
    CREATE TABLE STORE_CATEGORY (
      STORE_CATEGORY_KEY SERIAL PRIMARY KEY,
      NAME VARCHAR(255),
      DESCRIPTION VARCHAR(255),
      STATUS BOOLEAN
    );
  END IF; 
END $$;

-- Bảng STORE_OWNER
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'STORE_OWNER') 
  THEN 
    CREATE TABLE STORE_OWNER (
      STORE_OWNER_KEY SERIAL PRIMARY KEY,
      USER_KEY INT,
      OWNER_NAME VARCHAR(50),
      STORE_KEY INT,
      UNIQUE (USER_KEY)
    );
  END IF; 
END $$;

-- Bảng STORE_WITHDRAWAL
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'STORE_WITHDRAWAL') 
  THEN 
    CREATE TABLE STORE_WITHDRAWAL (
      WITHDRAWAL_KEY SERIAL PRIMARY KEY,
      STORE_KEY INT,
      AMOUNT INT,
      DATE DATE,
      STATUS BOOLEAN
    );
  END IF; 
END $$;

-- Bảng TOPUP_MEMBER
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'TOPUP_MEMBER') 
  THEN 
    CREATE TABLE TOPUP_MEMBER (
      TOPUP_KEY SERIAL PRIMARY KEY,
      USER_KEY INT,
      AMOUNT INT,
      DATE DATE,
      STATUS BOOLEAN
    );
  END IF; 
END $$;

-- Bảng USER
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'USER') 
  THEN 
    CREATE TABLE "USER" (
      USER_KEY SERIAL PRIMARY KEY,
      USERNAME VARCHAR(50),
      PASSWORD VARCHAR(50),
      ROLE INT,
      STATUS BOOLEAN
    );
  END IF; 
END $$;

-- Bảng WALLET
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.tables WHERE table_name = 'WALLET') 
  THEN 
    CREATE TABLE WALLET (
      WALLET_KEY SERIAL PRIMARY KEY,
      USER_KEY INT,
      BALANCE INT
    );
  END IF; 
END $$;

-- Thêm khóa ngoại
DO $$ 
BEGIN 
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_admin_user') 
  THEN 
    ALTER TABLE ADMIN ADD CONSTRAINT fk_admin_user FOREIGN KEY (USER_KEY) REFERENCES "USER" (USER_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_card_customer') 
  THEN 
    ALTER TABLE CARD ADD CONSTRAINT fk_card_customer FOREIGN KEY (CUSTOMER_KEY) REFERENCES CUSTOMER (CUSTOMER_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_card_card_type') 
  THEN 
    ALTER TABLE CARD ADD CONSTRAINT fk_card_card_type FOREIGN KEY (CARD_TYPE_KEY) REFERENCES CARD_TYPE (CARD_TYPE_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_customer_user') 
  THEN 
    ALTER TABLE CUSTOMER ADD CONSTRAINT fk_customer_user FOREIGN KEY (USER_KEY) REFERENCES "USER" (USER_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_card_store_category_card_type') 
  THEN 
    ALTER TABLE CARD_STORE_CATEGORY ADD CONSTRAINT fk_card_store_category_card_type FOREIGN KEY (CARD_TYPE_KEY) REFERENCES CARD_TYPE (CARD_TYPE_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_card_store_category_store_category') 
  THEN 
    ALTER TABLE CARD_STORE_CATEGORY ADD CONSTRAINT fk_card_store_category_store_category FOREIGN KEY (STORE_CATEGORY_KEY) REFERENCES STORE_CATEGORY (STORE_CATEGORY_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_store_store_category') 
  THEN 
    ALTER TABLE STORE ADD CONSTRAINT fk_store_store_category FOREIGN KEY (CATEGORY_KEY) REFERENCES STORE_CATEGORY (STORE_CATEGORY_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_store_owner_user') 
  THEN 
    ALTER TABLE STORE_OWNER ADD CONSTRAINT fk_store_owner_user FOREIGN KEY (USER_KEY) REFERENCES "USER" (USER_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_store_owner_store') 
  THEN 
    ALTER TABLE STORE_OWNER ADD CONSTRAINT fk_store_owner_store FOREIGN KEY (STORE_KEY) REFERENCES STORE (STORE_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_order_store') 
  THEN 
    ALTER TABLE "ORDER" ADD CONSTRAINT fk_order_store FOREIGN KEY (STORE_KEY) REFERENCES STORE (STORE_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_order_card') 
  THEN 
    ALTER TABLE "ORDER" ADD CONSTRAINT fk_order_card FOREIGN KEY (CARD_KEY) REFERENCES CARD (CARD_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_store_withdrawal_store') 
  THEN 
    ALTER TABLE STORE_WITHDRAWAL ADD CONSTRAINT fk_store_withdrawal_store FOREIGN KEY (STORE_KEY) REFERENCES STORE (STORE_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_topup_member_user') 
  THEN 
    ALTER TABLE TOPUP_MEMBER ADD CONSTRAINT fk_topup_member_user FOREIGN KEY (USER_KEY) REFERENCES "USER" (USER_KEY);
  END IF; 
  
  IF NOT EXISTS (SELECT 1 FROM information_schema.table_constraints WHERE constraint_name = 'fk_admin_user') 
  THEN 
    ALTER TABLE ADMIN ADD CONSTRAINT fk_admin_user FOREIGN KEY (USER_KEY) REFERENCES "USER" (USER_KEY);
  END IF; 
END $$;
